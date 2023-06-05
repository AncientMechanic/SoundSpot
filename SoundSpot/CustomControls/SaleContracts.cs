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
    public partial class SaleContracts : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private string table = "contractssale";
        private string tableid = "contractsaleid";
        public SaleContracts()
        {
            InitializeComponent();
            this.VisibleChanged += SaleContracts_VisibleChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Contracts contracts = parentForm.Controls["contracts1"] as Contracts;
            contracts.Visible = true;
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT s." + tableid + ", s.description, s.date, i.summary AS total, c.firstname || ' ' || c.lastname AS client, l.firstname || ' ' || l.lastname AS seller, i.number AS invoicenumber, i.payment AS paid, i.dispatch AS dispatched, " +
               "'Редактировать' AS Edit " +
               "FROM contractssale AS s " +
               "JOIN saleinvoices AS i ON i.contractsaleid = s.contractsaleid " +
               "JOIN clients AS c ON s.clientid = c.clientid " +
               "JOIN sellers AS l ON s.sellerid = l.sellerid";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                dataAdapter = adapter;

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, "Result");

                foreach (DataRow row in dataSet.Tables["Result"].Rows)
                {
                    int contractId = Convert.ToInt32(row["contractsaleid"]);
                    decimal totalSum = CalculateTotalSum(contractId);
                    row["total"] = totalSum;
                }

                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dataSet.Tables["Result"];
                ClientsGridView.DataSource = bindingSource;
                ClientsGridView.Sort(ClientsGridView.Columns[tableid], ListSortDirection.Ascending);

                ClientsGridView.Columns[tableid].HeaderText = "Номер договора";
                ClientsGridView.Columns["description"].HeaderText = "Договор";
                ClientsGridView.Columns["date"].HeaderText = "Дата";
                ClientsGridView.Columns["total"].HeaderText = "Сумма, руб.";
                ClientsGridView.Columns["client"].HeaderText = "Клиент";
                ClientsGridView.Columns["seller"].HeaderText = "Продавец";
                ClientsGridView.Columns["invoicenumber"].HeaderText = "Номер счета";
                ClientsGridView.Columns["paid"].HeaderText = "Оплачено";
                ClientsGridView.Columns["dispatched"].HeaderText = "Отгружено";
                ClientsGridView.Columns["Edit"].HeaderText = "Редактировать";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка LoadData!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private decimal CalculateTotalSum(int contractId)
        {
            string sumQuery = "SELECT SUM(summary) FROM orders WHERE contractsaleid = @contractsaleid";
            using (NpgsqlCommand sumCommand = new NpgsqlCommand(sumQuery, connection))
            {
                sumCommand.Parameters.AddWithValue("@contractsaleid", contractId);
                object result = sumCommand.ExecuteScalar();

                if (result != DBNull.Value && result != null)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    return 0;
                }
            }
        }

        private void OpenCustomControl(int editrowId)
        {
            try
            {
                string query = "SELECT s." + tableid + ", s.description, s.date, i.summary AS total, c.firstname || ' ' || c.lastname AS client, l.firstname || ' ' || l.lastname AS seller, i.number AS invoicenumber, i.payment AS paid, i.dispatch AS dispatched, " +
                    "'Редактировать' AS Edit " +
               "FROM contractssale AS s " +
               "JOIN saleinvoices AS i ON s.contractsaleid = i.contractsaleid " +
               "JOIN clients AS c ON s.clientid = c.clientid " +
               "JOIN sellers AS l ON s.sellerid = l.sellerid "+
                "WHERE  s." + tableid + " = @" + tableid;
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@" + tableid, editrowId);

                ClientsGridView.Columns[tableid].Visible = false;

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, table);

                if (dataSet.Tables[table].Rows.Count > 0)
                {
                    // Создать и открыть форму EditDataBooks
                    var editform = new CustomForms.EditSaleContract();
                    
                    string dateString = dataSet.Tables[table].Rows[0]["date"].ToString();
                    DateTime databaseDateTime = DateTime.Parse(dateString);
                    DateTime databaseDateOnly = databaseDateTime.Date;

                    LocalDate parsedDate = LocalDate.FromDateTime(databaseDateOnly);
                    editform.Description = dataSet.Tables[table].Rows[0]["description"].ToString();
                    editform.Date = parsedDate.ToDateTimeUnspecified();
                    editform.Sum = decimal.Parse(dataSet.Tables[table].Rows[0]["total"].ToString());
                    editform.Client = dataSet.Tables[table].Rows[0]["client"].ToString();
                    editform.Seller = dataSet.Tables[table].Rows[0]["seller"].ToString();
                    editform.Invoiceid = dataSet.Tables[table].Rows[0]["invoicenumber"].ToString();
                    editform.Payment = (bool)dataSet.Tables[table].Rows[0]["paid"];
                    editform.Dispatch = (bool)dataSet.Tables[table].Rows[0]["dispatched"];
                    DialogResult result = editform.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        DateTime updatedDateTime = editform.Date;

                        LocalDate updatedDate = LocalDate.FromDateTime(updatedDateTime);
                        bool updatedpaid = editform.Payment;
                        bool updateddispatched = editform.Dispatch;

                        // Обновить базу данных с новыми значениями
                        string updateQuery = "UPDATE contractssale SET date = @date, description = @description WHERE "+ tableid +" = @"+tableid;
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@date", updatedDate.ToDateTimeUnspecified());
                        updateCommand.Parameters.AddWithValue("@"+tableid, editrowId);
                        updateCommand.ExecuteNonQuery();

                        DataRow updatedRow = dataSet.Tables[table].Rows[0];
                        updatedRow["date"] = updatedDate.ToDateTimeUnspecified();

                        string updateQueryinvoice = "UPDATE saleinvoices SET payment = @payment, dispatch = @dispatch WHERE " + tableid + " = @" + tableid;
                        NpgsqlCommand updateCommandinvoice = new NpgsqlCommand(updateQueryinvoice, connection);
                        updateCommandinvoice.Parameters.AddWithValue("@payment", updatedpaid);
                        updateCommandinvoice.Parameters.AddWithValue("@dispatch", updateddispatched);
                        updateCommandinvoice.Parameters.AddWithValue("@" + tableid, editrowId);
                        updateCommandinvoice.ExecuteNonQuery();

                        int rowIndex = ClientsGridView.SelectedCells[0].RowIndex;
                        DataGridViewRow dataGridViewRow = ClientsGridView.Rows[rowIndex];
                        dataGridViewRow.Cells["date"].Value = updatedDate.ToDateTimeUnspecified();
                        dataGridViewRow.Cells["paid"].Value = updatedpaid;
                        dataGridViewRow.Cells["dispatched"].Value = updateddispatched;
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
            string query = "SELECT s." + tableid + ", s.description, s.date, i.summary AS total, c.firstname || ' ' || c.lastname AS client, l.firstname || ' ' || l.lastname AS seller, i.number AS invoicenumber, i.payment AS paid, i.dispatch AS dispatched, " +
                "'Редактировать' AS Edit " +
               "FROM contractssale AS s " +
               "JOIN saleinvoices AS i ON s.contractsaleid = i.contractsaleid " +
               "JOIN clients AS c ON s.clientid = c.clientid " +
               "JOIN sellers AS l ON s.sellerid = l.sellerid";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, table);

            foreach (DataRow row in dataSet.Tables["contractssale"].Rows)
            {
                int contractId = Convert.ToInt32(row["contractsaleid"]);
                decimal totalSum = CalculateTotalSum(contractId);
                row["total"] = totalSum;
            }

            ClientsGridView.DataSource = dataSet.Tables[table];
            ClientsGridView.Columns[tableid].Visible = false;
        }

        private void SaleContracts_Load(object sender, EventArgs e)
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
            var addDataForm = new CustomForms.AddSaleContract();
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

        private void SaleContracts_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // The control is now visible, so refresh the DataGridView
                RefreshDataGridView();
            }
        }
    }
}
