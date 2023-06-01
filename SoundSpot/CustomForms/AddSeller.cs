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
    public partial class AddSeller : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddSeller()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string firstname = textBox1.Text;
                string lastname = textBox2.Text;
                string middlename = textBox3.Text;
                int salary = int.Parse(textBox4.Text);



                // Вставить новую запись в таблицу
                string insertQuery = "INSERT INTO sellers (firstname, lastname, middlename, salary) VALUES (@firstname, @lastname, @middlename, @salary)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@firstname", firstname);
                insertCommand.Parameters.AddWithValue("@lastname", lastname);
                insertCommand.Parameters.AddWithValue("@middlename", middlename);
                insertCommand.Parameters.AddWithValue("@salary", salary);
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

        private void AddSeller_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }
    }
}
