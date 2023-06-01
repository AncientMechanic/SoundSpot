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
    public partial class AddSupplier : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddSupplier()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text;
                string address = textBox2.Text;
                


                // Вставить новую запись в таблицу
                string insertQuery = "INSERT INTO suppliers (name, address) VALUES (@name, @address)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@name", name);
                insertCommand.Parameters.AddWithValue("@address", address);
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

        private void AddSupplier_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }
    }
}
