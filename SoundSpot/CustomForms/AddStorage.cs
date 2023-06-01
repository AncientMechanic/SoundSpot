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
    public partial class AddStorage : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddStorage()
        {
            InitializeComponent();
        }

        public event EventHandler DataSaved;
        private int GetInstrumentIdByName(string InstrumentName)
        {
            string query = "SELECT InstrumentID FROM Instruments WHERE Name = @Name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", InstrumentName);
            int instrumentId = Convert.ToInt32(command.ExecuteScalar());
            return instrumentId;
        }


        private void AddStorage_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT Name FROM Instruments";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string publisherName = reader["Name"].ToString();
                    comboBox1.Items.Add(publisherName);
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


                int amount = int.Parse(textBox1.Text);

                string instrument = comboBox1.SelectedItem.ToString();


                // Получить идентификаторы связанных записей на основе выбранных значений
                int instrumentId = GetInstrumentIdByName(instrument);

                // Вставить новую запись в таблицу
                string insertQuery = "INSERT INTO Storage (Amount, InstrumentID) VALUES (@Amount, @InstrumentID)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Amount", amount);
                insertCommand.Parameters.AddWithValue("@InstrumentID", instrumentId);
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
