using NodaTime;
using Npgsql;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundSpot.CustomForms
{
    public partial class AddSaleContract : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddSaleContract()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;
        private int GetClientIdByName(string ClientName)
        {
            string[] names = ClientName.Split(' ');
            string firstname = names[0];
            string lastname = names[1];

            string query = "SELECT clientid FROM clients WHERE firstname = @firstname AND lastname = @lastname";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstname", firstname);
            command.Parameters.AddWithValue("@lastname", lastname);
            int ClientId = Convert.ToInt32(command.ExecuteScalar());
            return ClientId;
        }

        private int GetSellerIdByName(string SellerName)
        {
            string[] names = SellerName.Split(' ');
            string firstname = names[0];
            string lastname = names[1];

            string query = "SELECT sellerid FROM sellers WHERE firstname = @firstname AND lastname = @lastname";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@firstname", firstname);
            command.Parameters.AddWithValue("@lastname", lastname);
            int SellerId = Convert.ToInt32(command.ExecuteScalar());
            return SellerId;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string description = textBox1.Text;
                DateTime dateTime = dateTimePicker1.Value;
                LocalDateTime localTime = LocalDateTime.FromDateTime(dateTime);
                string client = comboBox1.SelectedItem.ToString();
                string seller = comboBox2.SelectedItem.ToString();
                string invoicenumber = textBox2.Text;
                bool paid = checkBox1.Checked;
                bool dispatch = checkBox2.Checked;

                // Get the identifiers of the related records based on the selected values
                int clientId = GetClientIdByName(client);
                int sellerId = GetSellerIdByName(seller);

                // Insert a new record into the contractssale table
                string insertContractQuery = "INSERT INTO contractssale (description, date, clientid, sellerid) VALUES (@description, @date, @clientid, @sellerid) RETURNING contractsaleid";
                NpgsqlCommand insertContractCommand = new NpgsqlCommand(insertContractQuery, connection);
                insertContractCommand.Parameters.AddWithValue("@description", description);
                insertContractCommand.Parameters.AddWithValue("@date", localTime.ToDateTimeUnspecified());
                insertContractCommand.Parameters.AddWithValue("@clientid", clientId);
                insertContractCommand.Parameters.AddWithValue("@sellerid", sellerId);
                int contractsaleid = Convert.ToInt32(insertContractCommand.ExecuteScalar());

                // Insert a new record into the saleinvoices table
                string insertInvoiceQuery = "INSERT INTO saleinvoices (number, payment, dispatch, summary, contractsaleid) VALUES (@number, @payment, @dispatch, @summary, @contractsaleid)";
                NpgsqlCommand insertInvoiceCommand = new NpgsqlCommand(insertInvoiceQuery, connection);
                insertInvoiceCommand.Parameters.AddWithValue("@number", invoicenumber);
                insertInvoiceCommand.Parameters.AddWithValue("@payment", paid);
                insertInvoiceCommand.Parameters.AddWithValue("@dispatch", dispatch);
                insertInvoiceCommand.Parameters.AddWithValue("@summary", 0); // Update with appropriate value
                insertInvoiceCommand.Parameters.AddWithValue("@contractsaleid", contractsaleid);
                insertInvoiceCommand.ExecuteNonQuery();

                MessageBox.Show("Договор успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataAdded?.Invoke(this, EventArgs.Empty);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddSaleContract_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT firstname, lastname FROM clients";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string comboitem = $"{reader["firstname"]} {reader["lastname"]}";
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
                string query = "SELECT firstname, lastname FROM sellers";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string comboitem = $"{reader["firstname"]} {reader["lastname"]}";
                    comboBox2.Items.Add(comboitem);
                }
                reader.Close();

                // Аналогично заполните ComboBox для других связанных таблиц
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка загрузки данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
