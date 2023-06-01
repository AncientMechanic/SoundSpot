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
    public partial class AddClient : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddClient()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;

        private void AddClient_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string firstname = textBox1.Text;
                string lastname = textBox2.Text;
                string middlename = textBox3.Text;
                string address = textBox4.Text;
                string bankaccount = textBox5.Text;
               

                // Вставить новую запись в таблицу
                string insertQuery = "INSERT INTO clients (firstname, lastname, middlename, address, bankaccount) VALUES (@firstname, @lastname, @middlename, @address, @bankaccount)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@firstname", firstname);
                insertCommand.Parameters.AddWithValue("@lastname", lastname);
                insertCommand.Parameters.AddWithValue("@middlename", middlename);
                insertCommand.Parameters.AddWithValue("@address", address);
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
