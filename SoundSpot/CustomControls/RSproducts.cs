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
using System.Diagnostics.Metrics;

namespace SoundSpot
{
    public partial class RSproducts : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        public RSproducts()
        {
            InitializeComponent();

            this.VisibleChanged += RSproducts_VisibleChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Instruments instruments = parentForm.Controls["instruments1"] as Instruments;
            instruments.Visible = true;
        }
        public void LoadDataSale(NpgsqlConnection connection)
        {
            try
            {
                string selectedOption = comboBox1.SelectedItem.ToString();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Кол-во товара");
                dataTable.Columns.Add("Товар");
                dataTable.Columns.Add("Производитель");
                dataTable.Columns.Add("Поставщик");
                dataTable.Columns.Add("Договор");

                string ordersTable = (selectedOption == "Sold Products") ? "orders" : "batches";
                string instrumentsTable = "instruments";
                string contractsTable = (selectedOption == "Sold Products") ? "contractssale" : "contractssupply";
                string invoicesTable = (selectedOption == "Sold Products") ? "saleinvoices" : "supplyinvoices";
                string contractIdColumn = (selectedOption == "Sold Products") ? "contractsaleid" : "contractsupplyid";

                // Get the invoice records that have payment and dispatch both set to true
                string invoicesQuery = $"SELECT * FROM {invoicesTable} WHERE payment = true AND dispatch = true";
                using (NpgsqlCommand invoicesCommand = new NpgsqlCommand(invoicesQuery, connection))
                {
                    NpgsqlDataAdapter invoicesAdapter = new NpgsqlDataAdapter(invoicesCommand);
                    DataTable invoicesDataTable = new DataTable();
                    invoicesAdapter.Fill(invoicesDataTable);

                    foreach (DataRow invoiceRow in invoicesDataTable.Rows)
                    {
                        int contractId = Convert.ToInt32(invoiceRow[contractIdColumn]);

                        // Get the order records for the current contract ID
                        string ordersQuery = $"SELECT * FROM {ordersTable} WHERE {contractIdColumn} = {contractId}";
                        using (NpgsqlCommand ordersCommand = new NpgsqlCommand(ordersQuery, connection))
                        {
                            NpgsqlDataAdapter ordersAdapter = new NpgsqlDataAdapter(ordersCommand);
                            DataTable ordersDataTable = new DataTable();
                            ordersAdapter.Fill(ordersDataTable);

                            foreach (DataRow orderRow in ordersDataTable.Rows)
                            {
                                int amount = Convert.ToInt32(orderRow["amount"]);
                                int instrumentId = Convert.ToInt32(orderRow["instrumentid"]);

                                // Get the instrument record for the current instrument ID
                                string instrumentsQuery = $"SELECT * FROM {instrumentsTable} WHERE instrumentid = {instrumentId}";
                                using (NpgsqlCommand instrumentsCommand = new NpgsqlCommand(instrumentsQuery, connection))
                                {
                                    NpgsqlDataAdapter instrumentsAdapter = new NpgsqlDataAdapter(instrumentsCommand);
                                    DataTable instrumentsDataTable = new DataTable();
                                    instrumentsAdapter.Fill(instrumentsDataTable);

                                    if (instrumentsDataTable.Rows.Count > 0)
                                    {
                                        DataRow instrumentRow = instrumentsDataTable.Rows[0];
                                        string instrumentName = instrumentRow["name"].ToString();

                                        int manufacturerId = Convert.ToInt32(instrumentRow["manufacturerid"]);
                                        string manufacturer = GetManufacturerNameById(manufacturerId, connection);

                                        int supplierId = Convert.ToInt32(instrumentRow["supplierid"]);
                                        string supplier = GetSupplierNameById(supplierId, connection);

                                        // Get the contract record for the current contract ID
                                        string contractsQuery = $"SELECT * FROM {contractsTable} WHERE {contractIdColumn} = {contractId}";
                                        using (NpgsqlCommand contractsCommand = new NpgsqlCommand(contractsQuery, connection))
                                        {
                                            NpgsqlDataAdapter contractsAdapter = new NpgsqlDataAdapter(contractsCommand);
                                            DataTable contractsDataTable = new DataTable();
                                            contractsAdapter.Fill(contractsDataTable);

                                            DataRow contractRow = contractsDataTable.Rows[0];
                                            string saleContract = contractRow["description"].ToString();

                                            dataTable.Rows.Add(amount, instrumentName, manufacturer, supplier, saleContract);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Bind the dataTable as the data source for the ClientsGridView
                ClientsGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in LoadDataSale!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetManufacturerNameById(int manufacturerId, NpgsqlConnection connection)
        {
            string name = string.Empty;

            try
            {
                string query = "SELECT name FROM manufacturers WHERE manufacturerid = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", manufacturerId);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        name = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in GetManufacturerNameById!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return name;
        }

        public string GetSupplierNameById(int supplierId, NpgsqlConnection connection)
        {
            string name = string.Empty;

            try
            {
                string query = "SELECT name FROM suppliers WHERE supplierid = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", supplierId);
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        name = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in GetSupplierNameById!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return name;
        }


        private void RSproducts_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            LoadDataSale(connection);

        }

        public void GenerateWordDocument(DataGridView dataGridView)
        {
            // Создание нового документа Word
            XWPFDocument document = new XWPFDocument();

            // Создание таблицы в документе
            XWPFTable table = document.CreateTable(dataGridView.Rows.Count + 1, dataGridView.Columns.Count);

            // Заполнение заголовков таблицы
            XWPFTableRow headerRow = table.GetRow(0);
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                string headerText = dataGridView.Columns[i].HeaderText;
                headerRow.GetCell(i).SetText(headerText);
            }

            // Заполнение таблицы данными из DataGridView
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                XWPFTableRow row = table.GetRow(i + 1);
                for (int j = 0; j < dataGridView.Columns.Count; j++)
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

        private void RSproducts_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // The control is now visible, so refresh the DataGridView
                RefreshDataGridView();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        public void RefreshDataGridView()
        {
            ClientsGridView.DataSource = null; // Clear the current data source
            ClientsGridView.Rows.Clear(); // Clear the rows

            LoadDataSale(connection);
        }
    }
}
