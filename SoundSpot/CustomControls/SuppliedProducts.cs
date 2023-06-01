using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Configuration;
using NPOI.XWPF.UserModel;
using DocumentFormat.OpenXml.Bibliography;

namespace SoundSpot
{
    public partial class SuppliedProducts : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private string table = "instruments";
        private string tableid = "instrumentid";
        public SuppliedProducts()
        {
            InitializeComponent();
            this.VisibleChanged += SuppliedProducts_VisibleChanged;
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
                string query = "SELECT i." + tableid + ", i.name, i.price, f.name AS type, m.name AS manufacturer, s.name AS supplier, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS i " +
               "JOIN families AS f ON i.familyid = f.familyid " +
               "JOIN manufacturers AS m ON i.manufacturerid = m.manufacturerid " +
               "JOIN suppliers AS s ON i.supplierid = s.supplierid";


                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                dataAdapter = adapter;

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, "Result");

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dataSet.Tables["Result"];
                ClientsGridView.DataSource = bindingSource;
                ClientsGridView.Columns[tableid].Visible = false;
                ClientsGridView.Sort(ClientsGridView.Columns[tableid], ListSortDirection.Ascending);

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
                string query = "SELECT i." + tableid + ", i.name, i.price, f.name AS type, m.name AS manufacturer, s.name AS supplier, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS i " +
               "JOIN families AS f ON i.familyid = f.familyid " +
               "JOIN manufacturers AS m ON i.manufacturerid = m.manufacturerid " +
               "JOIN suppliers AS s ON i.supplierid = s.supplierid " +
                "WHERE  i." + tableid + " = @" + tableid;
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@" + tableid, editrowId);

                ClientsGridView.Columns[tableid].Visible = false;

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, table);

                if (dataSet.Tables[table].Rows.Count > 0)
                {
                    // Создать и открыть форму EditDataBooks
                    var editform = new CustomForms.EditSuppliedProduct();
                    editform.Instrumentname = dataSet.Tables[table].Rows[0]["name"].ToString();
                    editform.Instrumentprice = decimal.Parse(dataSet.Tables[table].Rows[0]["price"].ToString());
                    editform.Instrumenttype = dataSet.Tables[table].Rows[0]["type"].ToString();
                    editform.Instrumentmanufacturer = dataSet.Tables[table].Rows[0]["manufacturer"].ToString();
                    editform.Instrumentsupplier = dataSet.Tables[table].Rows[0]["supplier"].ToString();

                    DialogResult result = editform.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string updatedName = editform.Instrumentname;
                        decimal updatedPrice = editform.Instrumentprice;

                        // Обновить базу данных с новыми значениями
                        string updateQuery = "UPDATE " + table + " SET name = @name, price = @price WHERE " + tableid + " = @" + tableid;
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@name", updatedName);
                        updateCommand.Parameters.AddWithValue("@price", updatedPrice);
                        updateCommand.Parameters.AddWithValue("@" + tableid, editrowId);

                        updateCommand.ExecuteNonQuery();

                        // Update the specific row in the DataSet with the new value
                        dataSet.Tables[table].Rows[0]["name"] = updatedName;
                        dataSet.Tables[table].Rows[0]["price"] = updatedPrice;

                        // Update the DataGridView cell with the new value
                        int rowIndex = ClientsGridView.SelectedCells[0].RowIndex;
                        ClientsGridView.Rows[rowIndex].Cells["name"].Value = updatedName;
                        ClientsGridView.Rows[rowIndex].Cells["price"].Value = updatedPrice;

                        // Обновите остальные ячейки в соответствии с обновлениями

                        // Очистите выделение в DataGridView
                        ClientsGridView.ClearSelection();
                    }
                }
                else
                {
                    MessageBox.Show("Книга с указанным названием не найдена.", "Ошибка OpenCustomControl!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка OpenCustomControl!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RefreshDataGridView()
        {
            string query = "SELECT i." + tableid + ", i.name, i.price, f.name AS type, m.name AS manufacturer, s.name AS supplier, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS i " +
               "JOIN families AS f ON i.familyid = f.familyid " +
               "JOIN manufacturers AS m ON i.manufacturerid = m.manufacturerid " +
               "JOIN suppliers AS s ON i.supplierid = s.supplierid";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, table);


            ClientsGridView.DataSource = dataSet.Tables[table];
            ClientsGridView.Columns[tableid].Visible = false;
        }

        private void SuppliedProducts_Load(object sender, EventArgs e)
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
            for (int i = 1; i < dataGridView.Columns.Count - 1; i++)
            {
                string headerText = dataGridView.Columns[i].HeaderText;
                headerRow.GetCell(i).SetText(headerText);
            }

            // Заполнение таблицы данными из DataGridView
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                XWPFTableRow row = table.GetRow(i + 1);
                for (int j = 1; j < dataGridView.Columns.Count - 1; j++)
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
            var addDataForm = new CustomForms.AddSuppliedProduct();
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

        private void SuppliedProducts_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // The control is now visible, so refresh the DataGridView
                RefreshDataGridView();
            }
        }
    }
}
