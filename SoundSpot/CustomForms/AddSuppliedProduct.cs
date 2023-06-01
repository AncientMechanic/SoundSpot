using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SoundSpot.CustomForms
{
    public partial class AddSuppliedProduct : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddSuppliedProduct()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;
        private int GetFamilyIdByName(string FamilyName)
        {
            string query = "SELECT familyid FROM families WHERE name = @name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", FamilyName);
            int familytId = Convert.ToInt32(command.ExecuteScalar());
            return familytId;
        }
        private int GetManufacturerIdByName(string ManufacturerName)
        {
            string query = "SELECT manufacturerid FROM manufacturers WHERE name = @name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", ManufacturerName);
            int manufacturerId = Convert.ToInt32(command.ExecuteScalar());
            return manufacturerId;
        }
        private int GetSupplierIdByName(string SupplierName)
        {
            string query = "SELECT supplierid FROM suppliers WHERE name = @name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", SupplierName);
            int SupplierId = Convert.ToInt32(command.ExecuteScalar());
            return SupplierId;
        }
        private void AddSuppliedProduct_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT Name FROM families";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string comboitem = reader["Name"].ToString();
                    comboBox1.Items.Add(comboitem);
                }
                reader.Close();

                // Аналогично заполните ComboBox для других связанных таблиц
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT Name FROM manufacturers";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string comboitem = reader["Name"].ToString();
                    comboBox2.Items.Add(comboitem);
                }
                reader.Close();

                // Аналогично заполните ComboBox для других связанных таблиц
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT Name FROM suppliers";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string comboitem = reader["Name"].ToString();
                    comboBox3.Items.Add(comboitem);
                }
                reader.Close();

                // Аналогично заполните ComboBox для других связанных таблиц
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                int price = int.Parse(textBox2.Text);
                string type = comboBox1.SelectedItem.ToString();
                string manufacturer = comboBox2.SelectedItem.ToString();
                string supplier = comboBox3.SelectedItem.ToString();

                int familyId = GetFamilyIdByName(type);
                int manufacturerId = GetManufacturerIdByName(manufacturer);
                int supplierId = GetSupplierIdByName(supplier);
                // Вставить новую запись в таблицу
                string insertQuery = "INSERT INTO instruments (name, price, familyid, manufacturerid, supplierid) VALUES (@name, @price, @familyid, @manufacturerid, @supplierid)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@price", price);
                insertCommand.Parameters.AddWithValue("@familyid", familyId);
                insertCommand.Parameters.AddWithValue("@manufacturerid", manufacturerId);
                insertCommand.Parameters.AddWithValue("@supplierid", supplierId);
                insertCommand.ExecuteNonQuery();

                // Обновить отображение в DataGridView
                DataAdded?.Invoke(this, EventArgs.Empty);
                Close();
                // Предполагается, что у вас есть DataGridView с именем BooksDataGridView

                MessageBox.Show("Данные успешно добавлены!", "Добавление данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
