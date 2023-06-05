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
using SoundSpot.CustomForms;

namespace SoundSpot
{
    public partial class Sellers : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private string table = "sellers";
        private string tableid = "sellerid";
        public Sellers()
        {
            InitializeComponent();
            this.VisibleChanged += Sellers_VisibleChanged;
        }
        private void LoadData()
        {
            try
            {
                string query = "SELECT s." + tableid + ", s.firstname, s.lastname, s.middlename, s.salary, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS s ";


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

                ClientsGridView.Columns["firstname"].HeaderText = "Имя";
                ClientsGridView.Columns["lastname"].HeaderText = "Фамилия";
                ClientsGridView.Columns["middlename"].HeaderText = "Отчество";
                ClientsGridView.Columns["salary"].HeaderText = "Зар.плата";
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
                string query = "SELECT s." + tableid + ", s.firstname, s.lastname, s.middlename, s.salary, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS s " +
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
                    var editform = new CustomForms.EditSeller();
                    editform.Sellerfirstname = dataSet.Tables[table].Rows[0]["firstname"].ToString();
                    editform.Sellerlastname = dataSet.Tables[table].Rows[0]["lastname"].ToString();
                    editform.Sellermiddlename = dataSet.Tables[table].Rows[0]["middlename"].ToString();
                    editform.Sellersalary = decimal.Parse(dataSet.Tables[table].Rows[0]["salary"].ToString());



                    DialogResult result = editform.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        string updatedfirstname = editform.Sellerfirstname;
                        string updatedlastname = editform.Sellerlastname;
                        string updatedmiddlename = editform.Sellermiddlename;
                        decimal updatedsalary = editform.Sellersalary;

                        // Обновить базу данных с новыми значениями
                        string updateQuery = "UPDATE " + table + " SET firstname = @firstname, lastname = @lastname, middlename = @middlename, salary = @salary WHERE " + tableid + " = @" + tableid;
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);

                        updateCommand.Parameters.AddWithValue("@firstname", updatedfirstname);
                        updateCommand.Parameters.AddWithValue("@lastname", updatedlastname);
                        updateCommand.Parameters.AddWithValue("@middlename", updatedmiddlename);
                        updateCommand.Parameters.AddWithValue("@salary", updatedsalary);
                        updateCommand.Parameters.AddWithValue("@" + tableid, editrowId);

                        updateCommand.ExecuteNonQuery();

                        // Update the specific row in the DataSet with the new value
                        dataSet.Tables[table].Rows[0]["firstname"] = updatedfirstname;
                        dataSet.Tables[table].Rows[0]["lastname"] = updatedlastname;
                        dataSet.Tables[table].Rows[0]["middlename"] = updatedmiddlename;
                        dataSet.Tables[table].Rows[0]["salary"] = updatedsalary;

                        // Update the DataGridView cell with the new value
                        int rowIndex = ClientsGridView.SelectedCells[0].RowIndex;
                        ClientsGridView.Rows[rowIndex].Cells["firstname"].Value = updatedfirstname;
                        ClientsGridView.Rows[rowIndex].Cells["lastname"].Value = updatedlastname;
                        ClientsGridView.Rows[rowIndex].Cells["middlename"].Value = updatedmiddlename;
                        ClientsGridView.Rows[rowIndex].Cells["salary"].Value = updatedsalary;


                        // Обновите остальные ячейки в соответствии с обновлениями

                        // Очистите выделение в DataGridView
                        ClientsGridView.ClearSelection();
                    }
                }
                else
                {
                    MessageBox.Show("Продавец с указанным названием не найден.", "Ошибка OpenCustomControl!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка OpenCustomControl!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshDataGridView()
        {
            string query = "SELECT s." + tableid + ", s.firstname, s.lastname, s.middlename, s.salary, " +
                    "'Редактировать' AS Edit " +
               "FROM " + table + " AS s ";

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, table);


            ClientsGridView.DataSource = dataSet.Tables[table];
            ClientsGridView.Columns[tableid].Visible = false;
        }

        private void Sellers_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            MainMenu mainMenu = parentForm.Controls["mainMenu1"] as MainMenu;
            mainMenu.Visible = true;
        }

        private void OpenAddDataForm()
        {
            var addDataForm = new CustomForms.AddSeller();
            addDataForm.DataAdded += AddDataForm_DataAdded;
            addDataForm.ShowDialog();
        }
        private void AddDataForm_DataAdded(object sender, EventArgs e)
        {
            // Обновление данных в DataGridView
            RefreshDataGridView();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenAddDataForm();
            ClientsGridView.Sort(ClientsGridView.Columns[tableid], ListSortDirection.Ascending);
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
        private void button3_Click(object sender, EventArgs e)
        {
            GenerateWordDocument(ClientsGridView);
        }

        private void Sellers_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                // The control is now visible, so refresh the DataGridView
                RefreshDataGridView();
            }
        }
    }
}
