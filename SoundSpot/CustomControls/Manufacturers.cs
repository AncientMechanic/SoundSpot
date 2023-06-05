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

namespace SoundSpot
{
    public partial class Manufacturers : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private string table = "manufacturers";
        private string tableid = "manufacturerid";
        public Manufacturers()
        {
            InitializeComponent();
            this.VisibleChanged += Manufacturers_VisibleChanged;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            MainMenu mainMenu = parentForm.Controls["mainMenu1"] as MainMenu;
            mainMenu.Visible = true;
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT m." + tableid + ", m.name, m.address, m.director, m.bankaccount, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS m ";


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

                ClientsGridView.Columns["name"].HeaderText = "Производитель";
                ClientsGridView.Columns["address"].HeaderText = "Адрес";
                ClientsGridView.Columns["director"].HeaderText = "Директор";
                ClientsGridView.Columns["bankaccount"].HeaderText = "Номер счета";
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
                string query = "SELECT m." + tableid + ", m.name, m.address, m.director, m.bankaccount, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS m " +
                "WHERE  m." + tableid + " = @" + tableid;

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("@" + tableid, editrowId);

                ClientsGridView.Columns[tableid].Visible = false;

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, table);

                if (dataSet.Tables[table].Rows.Count > 0)
                {
                    // Создать и открыть форму EditDataBooks
                    var editform = new CustomForms.EditManufacturer();
                    editform.Manufacturername = dataSet.Tables[table].Rows[0]["name"].ToString();
                    editform.Manufactureraddress = dataSet.Tables[table].Rows[0]["address"].ToString();
                    editform.Manufacturerdirector = dataSet.Tables[table].Rows[0]["director"].ToString();
                    editform.Manufacturerbankaccount = dataSet.Tables[table].Rows[0]["bankaccount"].ToString();

                    DialogResult result = editform.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string updatedname = editform.Manufacturername;
                        string updatedaddress = editform.Manufactureraddress;
                        string updateddirector = editform.Manufacturerdirector;
                        string updatedbankaccount = editform.Manufacturerbankaccount;


                        // Обновить базу данных с новыми значениями
                        string updateQuery = "UPDATE " + table + " SET name = @name, address = @address, director = @director, bankaccount = @bankaccount WHERE " + tableid + " = @" + tableid;
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);

                        updateCommand.Parameters.AddWithValue("@name", updatedname);
                        updateCommand.Parameters.AddWithValue("@address", updatedaddress);
                        updateCommand.Parameters.AddWithValue("@director", updateddirector);
                        updateCommand.Parameters.AddWithValue("@bankaccount", updatedbankaccount);
                        updateCommand.Parameters.AddWithValue("@" + tableid, editrowId);

                        updateCommand.ExecuteNonQuery();

                        // Update the specific row in the DataSet with the new value
                        dataSet.Tables[table].Rows[0]["name"] = updatedname;
                        dataSet.Tables[table].Rows[0]["address"] = updatedaddress;
                        dataSet.Tables[table].Rows[0]["director"] = updateddirector;
                        dataSet.Tables[table].Rows[0]["bankaccount"] = updatedbankaccount;

                        // Update the DataGridView cell with the new value
                        int rowIndex = ClientsGridView.SelectedCells[0].RowIndex;
                        ClientsGridView.Rows[rowIndex].Cells["name"].Value = updatedname;
                        ClientsGridView.Rows[rowIndex].Cells["address"].Value = updatedaddress;
                        ClientsGridView.Rows[rowIndex].Cells["director"].Value = updateddirector;
                        ClientsGridView.Rows[rowIndex].Cells["bankaccount"].Value = updatedbankaccount;


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
            string query = "SELECT m." + tableid + ", m.name, m.address, m.director, m.bankaccount, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS m ";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, table);


            ClientsGridView.DataSource = dataSet.Tables[table];
            ClientsGridView.Columns[tableid].Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            OpenAddDataForm();
            ClientsGridView.Sort(ClientsGridView.Columns[tableid], ListSortDirection.Ascending);
        }
        private void OpenAddDataForm()
        {
            var addDataForm = new CustomForms.AddManufacturer();
            addDataForm.DataAdded += AddDataForm_DataAdded;
            addDataForm.ShowDialog();
        }
        private void AddDataForm_DataAdded(object sender, EventArgs e)
        {
            // Обновление данных в DataGridView
            RefreshDataGridView();
        }
        private void Manufacturers_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateWordDocument(ClientsGridView);
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

        private void Manufacturers_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // The control is now visible, so refresh the DataGridView
                RefreshDataGridView();
            }
        }
    }
}
