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
    public partial class AddShipOrder : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        public AddShipOrder()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;
        private int GetSupplyContractIdByName(string SupplyContractDesc)
        {
            string query = "SELECT contractsupplyid FROM contractssupply WHERE description = @description";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@description", SupplyContractDesc);
            int supplycontractid = Convert.ToInt32(command.ExecuteScalar());
            return supplycontractid;
        }
        private int GetInstrumentIdByName(string InstrumentName)
        {
            string query = "SELECT InstrumentID FROM Instruments WHERE Name = @Name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", InstrumentName);
            int instrumentId = Convert.ToInt32(command.ExecuteScalar());
            return instrumentId;
        }
        private void AddToStoreroom(int addCount, int instrumentid, int supCon)
        {
            try
            {
                string checkQuery = "SELECT payment, dispatch FROM supplyinvoices WHERE contractsupplyid = @contractsupplyid";
                NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@contractsupplyid", supCon);
                NpgsqlDataReader reader = checkCommand.ExecuteReader();

                bool paid = false;
                bool dispatched = false;
                if (reader.Read())
                {
                    paid = reader.GetBoolean(0);
                    dispatched = reader.GetBoolean(1);
                }
                reader.Close();
                if (paid && dispatched)
                {
                    // Получить текущее значение из поля count в таблице storeroom
                    string selectQuery = "SELECT amount FROM storage";
                    NpgsqlCommand selectCommand = new NpgsqlCommand(selectQuery, connection);
                    decimal currentCount = (int)selectCommand.ExecuteScalar();

                    // Проверить, есть ли достаточное количество для вычитания

                    string updateQuery = "UPDATE storage SET amount = amount + @addCount WHERE  instrumentid = @instrumentid";
                    NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@addCount", addCount);
                    updateCommand.Parameters.AddWithValue("@instrumentid", instrumentid);
                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления в базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int amount = int.Parse(textBox1.Text);
                string supplycontract = comboBox1.SelectedItem.ToString();
                string instrument = comboBox2.SelectedItem.ToString();

                // Get the identifiers of the related records based on the selected values
                int supplyConId = GetSupplyContractIdByName(supplycontract);
                int instrumentId = GetInstrumentIdByName(instrument);

                // Get the price from the instruments table
                string priceQuery = "SELECT price FROM instruments WHERE InstrumentID = @InstrumentID";
                NpgsqlCommand priceCommand = new NpgsqlCommand(priceQuery, connection);
                priceCommand.Parameters.AddWithValue("@InstrumentID", instrumentId);
                decimal price = Convert.ToDecimal(priceCommand.ExecuteScalar());

                decimal total = amount * price;

                int addCount = int.Parse(textBox1.Text);
                AddToStoreroom(addCount, instrumentId, supplyConId);

                // Insert a new record into the orders table
                string insertQuery = "INSERT INTO batches (amount, summary, contractsupplyid, instrumentid) VALUES (@amount, @summary, @contractsupplyid, @instrumentid)";
                NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@amount", amount);
                insertCommand.Parameters.AddWithValue("@summary", total);
                insertCommand.Parameters.AddWithValue("@contractsupplyid", supplyConId);
                insertCommand.Parameters.AddWithValue("@instrumentid", instrumentId);
                insertCommand.ExecuteNonQuery();

                MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataAdded?.Invoke(this, EventArgs.Empty);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddShipOrder_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT description FROM contractssupply";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string comboitem = reader["description"].ToString();
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
                string query = "SELECT Name FROM Instruments";
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
        }
    }
}
