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
    public partial class AddSupplyContract : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddSupplyContract()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;
        private int GetSupplierIdByName(string SupplierName)
        {
            string query = "SELECT supplierid FROM suppliers WHERE name = @name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", SupplierName);
            int SupplierId = Convert.ToInt32(command.ExecuteScalar());
            return SupplierId;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string description = textBox1.Text;
                DateTime dateTime = dateTimePicker1.Value;
                LocalDateTime localTime = LocalDateTime.FromDateTime(dateTime);
                string supplier = comboBox1.SelectedItem.ToString();
                string invoicenumber = textBox2.Text;
                bool paid = checkBox1.Checked;
                bool dispatch = checkBox2.Checked;

                // Get the identifiers of the related records based on the selected values
                int supplierid = GetSupplierIdByName(supplier);

                string insertContractQuery = "INSERT INTO contractssupply (description, date, supplierid) VALUES (@description, @date, @supplierid) RETURNING contractsupplyid";
                NpgsqlCommand insertContractCommand = new NpgsqlCommand(insertContractQuery, connection);
                insertContractCommand.Parameters.AddWithValue("@description", description);
                insertContractCommand.Parameters.AddWithValue("@date", localTime.ToDateTimeUnspecified());
                insertContractCommand.Parameters.AddWithValue("@supplierid", supplierid);
                int contractsupplyid = Convert.ToInt32(insertContractCommand.ExecuteScalar());


                string insertInvoiceQuery = "INSERT INTO supplyinvoices (number, payment, dispatch, summary, contractsupplyid) VALUES (@number, @payment, @dispatch, @summary, @contractsupplyid)";
                NpgsqlCommand insertInvoiceCommand = new NpgsqlCommand(insertInvoiceQuery, connection);
                insertInvoiceCommand.Parameters.AddWithValue("@number", invoicenumber);
                insertInvoiceCommand.Parameters.AddWithValue("@payment", paid);
                insertInvoiceCommand.Parameters.AddWithValue("@dispatch", dispatch);
                insertInvoiceCommand.Parameters.AddWithValue("@summary", 0); // Update with appropriate value
                insertInvoiceCommand.Parameters.AddWithValue("@contractsupplyid", contractsupplyid);
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

        private void AddSupplyContract_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT Name FROM suppliers";
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
        }
    }
}
