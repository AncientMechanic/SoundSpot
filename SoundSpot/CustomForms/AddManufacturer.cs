using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SoundSpot.CustomForms
{
    public partial class AddManufacturer : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddManufacturer()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;
        private void AddManufacturer_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox2.Text;
                string address = textBox3.Text;
                string director = textBox4.Text;
                string bankaccount = textBox5.Text;


                // Вставить новую запись в таблицу
                string insertQuery = "INSERT INTO manufacturers (name, address, director, bankaccount) VALUES (@name, @address, @director, @bankaccount)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@address", address);
                insertCommand.Parameters.AddWithValue("@director", director);
                insertCommand.Parameters.AddWithValue("@bankaccount", bankaccount);
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
