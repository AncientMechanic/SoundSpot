using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using Npgsql;
using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NodaTime.Text;
using NPOI.XWPF.UserModel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace SoundSpot
{
    public partial class ShipOrders : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private string table = "batches";
        private string tableid = "batchid";
        public ShipOrders()
        {
            InitializeComponent();
            this.VisibleChanged += ShipOrders_VisibleChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Instruments instruments = parentForm.Controls["instruments1"] as Instruments;
            instruments.Visible = true;
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT b.batchid, b.amount, b.summary, s.description AS supplycontract, i.name AS instrument, " +
                    "'Редактировать' AS Edit " +
               "FROM batches AS b " +
               "JOIN contractssupply AS s ON b.contractsupplyid = s.contractsupplyid " +
               "JOIN instruments AS i ON b.instrumentid = i.instrumentid";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                dataAdapter = adapter;

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, "Result");

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dataSet.Tables["Result"];
                ClientsGridView.DataSource = bindingSource;
                ClientsGridView.Sort(ClientsGridView.Columns[tableid], ListSortDirection.Ascending);

                ClientsGridView.Columns[tableid].HeaderText = "Номер заказа";
                ClientsGridView.Columns["amount"].HeaderText = "Кол-во товара";
                ClientsGridView.Columns["summary"].HeaderText = "Сумма, руб.";
                ClientsGridView.Columns["supplycontract"].HeaderText = "Договор";
                ClientsGridView.Columns["instrument"].HeaderText = "Товар";
                ClientsGridView.Columns["Edit"].HeaderText = "Редактировать";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка LoadData!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCustomControl(int editrowId)
        {
            try
            {
                string query = "SELECT b.batchid, b.amount, b.summary, s.description AS supplycontract, i.name AS instrument, " +
                    "'Редактировать' AS Edit " +
               "FROM batches AS b " +
               "JOIN contractssupply AS s ON b.contractsupplyid = s.contractsupplyid " +
               "JOIN instruments AS i ON b.instrumentid = i.instrumentid " +
                "WHERE  b." + tableid + " = @" + tableid;
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@" + tableid, editrowId);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, table);

                if (dataSet.Tables[table].Rows.Count > 0)
                {
                    // Создать и открыть форму EditDataBooks
                    var editform = new CustomForms.EditShipOrder();
                    editform.OrderName = editrowId;
                    editform.Amount = decimal.Parse(dataSet.Tables[table].Rows[0]["amount"].ToString());
                    editform.Sum = decimal.Parse(dataSet.Tables[table].Rows[0]["summary"].ToString());
                    editform.Contract = dataSet.Tables[table].Rows[0]["supplycontract"].ToString();
                    editform.Instrumentname = dataSet.Tables[table].Rows[0]["instrument"].ToString();

                    DialogResult result = editform.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        decimal updatedAmount = editform.Amount;
                        decimal updatedSum = editform.Sum;

                        // Обновить базу данных с новыми значениями
                        string updateQuery = "UPDATE batches SET amount = @amount, summary = @summary WHERE batchid = @batchid";
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@amount", updatedAmount);
                        updateCommand.Parameters.AddWithValue("@summary", updatedSum);
                        updateCommand.Parameters.AddWithValue("@batchid", editrowId);
                        updateCommand.ExecuteNonQuery();

                        DataRow updatedRow = dataSet.Tables["batches"].Rows[0];
                        updatedRow["amount"] = updatedAmount;
                        updatedRow["summary"] = updatedSum;

                        int rowIndex = ClientsGridView.SelectedCells[0].RowIndex;
                        DataGridViewRow dataGridViewRow = ClientsGridView.Rows[rowIndex];
                        dataGridViewRow.Cells["amount"].Value = updatedAmount;
                        dataGridViewRow.Cells["summary"].Value = updatedSum;
                        // Обновите остальные ячейки в соответствии с обновлениями

                        // Очистите выделение в DataGridView
                        ClientsGridView.ClearSelection();
                    }
                }
                else
                {
                    MessageBox.Show("Заказ с указанным названием не найдена.", "Ошибка OpenCustomControl!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка OpenCustomControl!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RefreshDataGridView()
        {
            string query = "SELECT b.batchid, b.amount, b.summary, s.description AS supplycontract, i.name AS instrument, " +
                    "'Редактировать' AS Edit " +
               "FROM batches AS b " +
               "JOIN contractssupply AS s ON b.contractsupplyid = s.contractsupplyid " +
               "JOIN instruments AS i ON b.instrumentid = i.instrumentid";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, table);


            ClientsGridView.DataSource = dataSet.Tables[table];
        }

        private void ShipOrders_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            LoadData();
        }

        public void GenerateWordDocument(DataGridView dataGridView)
        {
            // Создание нового документа Word
            XWPFDocument document = new XWPFDocument();

            // Создание таблицы в документе
            XWPFTable table = document.CreateTable(dataGridView.Rows.Count, dataGridView.Columns.Count - 1);

            // Заполнение заголовков таблицы
            XWPFTableRow headerRow = table.GetRow(0);
            for (int i = 0; i < dataGridView.Columns.Count - 1; i++)
            {
                string headerText = dataGridView.Columns[i].HeaderText;
                headerRow.GetCell(i).SetText(headerText);
            }

            // Заполнение таблицы данными из DataGridView
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                XWPFTableRow row = table.GetRow(i + 1);
                for (int j = 0; j < dataGridView.Columns.Count - 1; j++)
                {
                    string cellValue = dataGridView.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
                    row.GetCell(j).SetText(cellValue);
                }
            }

            // Отображение диалогового окна выбора пути сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Документ Word (*.docx)|*.docx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Сохранение документа в выбранный путь
                using (FileStream fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write))
                {
                    document.Write(fileStream);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateWordDocument(ClientsGridView);
        }

        private void OpenAddDataForm()
        {
            var addDataForm = new CustomForms.AddShipOrder();
            addDataForm.DataAdded += AddDataForm_DataAdded;
            addDataForm.ShowDialog();
        }
        private void AddDataForm_DataAdded(object sender, EventArgs e)
        {
            // Обновление данных в DataGridView
            RefreshDataGridView();
        }

        private void ClientsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == ClientsGridView.Columns["Edit"].Index)
                {
                    int convar = (int)ClientsGridView.Rows[e.RowIndex].Cells[tableid].Value;
                    OpenCustomControl(convar);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка CellContentClick!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenAddDataForm();
            ClientsGridView.Sort(ClientsGridView.Columns[tableid], ListSortDirection.Ascending);
        }

        private void ShipOrders_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // The control is now visible, so refresh the DataGridView
                //RefreshDataGridView();
            }
        }
    }
}
