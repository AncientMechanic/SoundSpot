using System;
using System.Collections.Generic;
using NodaTime.Text;
using NodaTime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Configuration;

namespace SoundSpot.CustomForms
{
    public partial class EditSaleContract : Form
    {
        private NpgsqlConnection? connection = null;
        public EditSaleContract()
        {
            InitializeComponent();
        }
        public string Description
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value.ToString(); }
        }
        public DateTime Date
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }
        public decimal Sum
        {
            get { return decimal.Parse(textBox5.Text); }
            set { textBox5.Text = value.ToString(); }
        }
        public string Client
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string Seller
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public string Invoiceid
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public bool Payment
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }
        public bool Dispatch
        {
            get { return checkBox2.Checked; }
            set { checkBox2.Checked = value; }
        }
        private void SubtractToStorage()
        {
            try
            {
                string infCon = textBox1.Text;
                string querySet = "SELECT contractsaleid FROM contractssale WHERE description = @description";
                NpgsqlCommand commandSet = new NpgsqlCommand(querySet, connection);
                commandSet.Parameters.AddWithValue("@description", infCon);
                int conId = Convert.ToInt32(commandSet.ExecuteScalar());

                string selectQuery = "SELECT instrumentid, amount FROM orders WHERE contractsaleid = @contractsaleid";
                NpgsqlCommand selectCommand = new NpgsqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@contractsaleid", conId);
                NpgsqlDataReader reader = selectCommand.ExecuteReader();

                Dictionary<int, decimal> bookCounts = new Dictionary<int, decimal>();

                while (reader.Read())
                {
                    int bookId = reader.GetInt32(0);
                    decimal count = reader.GetDecimal(1);

                    if (bookCounts.ContainsKey(bookId))
                    {
                        bookCounts[bookId] += count;
                    }
                    else
                    {
                        bookCounts.Add(bookId, count);
                    }
                }

                reader.Close();

                foreach (var kvp in bookCounts)
                {
                    int bookId = kvp.Key;
                    decimal orderCount = kvp.Value;

                    // Получить текущее значение из поля count в таблице storeroom для данного bookId
                    string currentCountQuery = "SELECT amount FROM storage WHERE instrumentid = @instrumentid";
                    NpgsqlCommand currentCountCommand = new NpgsqlCommand(currentCountQuery, connection);
                    currentCountCommand.Parameters.AddWithValue("@instrumentid", bookId);
                    decimal currentCount = Convert.ToDecimal(currentCountCommand.ExecuteScalar());

                    if (currentCount >= orderCount)
                    {
                        // Выполнить обновление в таблице storeroom для данного bookId
                        string updateQuery = "UPDATE storage SET amount = amount - @orderCount WHERE instrumentid = @instrumentid";
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@orderCount", orderCount);
                        updateCommand.Parameters.AddWithValue("@instrumentid", bookId);
                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        Payment = false;
                        MessageBox.Show("Недостаточное количество книг на складе.", "Ошибка добавления в базу данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Выход из цикла или обработка ошибки недостаточного количества книг
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления в базу данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditSaleContract_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SubtractToStorage();
            Close();
        }
    }
}
