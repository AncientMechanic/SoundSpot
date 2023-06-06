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
    public partial class AddClientOrder : Form
    {
        private NpgsqlConnection? connection = null;
        public event EventHandler DataAdded;
        private bool isEnough;
        public AddClientOrder()
        {
            InitializeComponent();
        }
        public event EventHandler DataSaved;
        private int GetSaleContractIdByName(string SaleContractDesc)
        {
            string query = "SELECT contractsaleid FROM contractssale WHERE description = @description";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@description", SaleContractDesc);
            int salecontractid = Convert.ToInt32(command.ExecuteScalar());
            return salecontractid;
        }
        private int GetInstrumentIdByName(string InstrumentName)
        {
            string query = "SELECT InstrumentID FROM Instruments WHERE Name = @Name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", InstrumentName);
            int instrumentId = Convert.ToInt32(command.ExecuteScalar());
            return instrumentId;
        }
        private void SubtractFromStoreroom(int subtractCount, int instrumentid, int salesConId)
        {
            try
            {
                string checkQuery = "SELECT payment, dispatch FROM saleinvoices WHERE contractsaleid = @contractsaleid";
                NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@contractsaleid", salesConId);
                NpgsqlDataReader reader = checkCommand.ExecuteReader();

                bool paid = false;
                //bool dispatched = false;
                if (reader.Read())
                {
                    paid = reader.GetBoolean(0);
                    //dispatched = reader.GetBoolean(1);
                }
                reader.Close();
                if (paid)
                {
                    // Получить текущее значение из поля count в таблице storeroom
                    string selectQuery = "SELECT amount FROM storage WHERE instrumentid = @instrumentid";
                    NpgsqlCommand selectCommand = new NpgsqlCommand(selectQuery, connection);
                    selectCommand.Parameters.AddWithValue("@instrumentid", instrumentid);
                    decimal currentCount = (int)selectCommand.ExecuteScalar();

                    // Проверить, есть ли достаточное количество для вычитания
                    if (currentCount >= subtractCount)
                    {
                        string updateQuery = "UPDATE storage SET amount = amount - @subtractCount WHERE  instrumentid = @instrumentid";
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@subtractCount", subtractCount);
                        updateCommand.Parameters.AddWithValue("@instrumentid", instrumentid);
                        updateCommand.ExecuteNonQuery();
                        isEnough = true;
                    }
                    else
                    {
                        isEnough = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка вычитания из базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateTime = dateTimePicker1.Value;
                LocalDateTime localTime = LocalDateTime.FromDateTime(dateTime);
                int amount = int.Parse(textBox1.Text);
                int total = int.Parse(textBox2.Text);
                string salecontract = comboBox1.SelectedItem.ToString();
                string instrument = comboBox2.SelectedItem.ToString();


                // Получить идентификаторы связанных записей на основе выбранных значений
                int salesConId = GetSaleContractIdByName(salecontract);
                int instrumentId = GetInstrumentIdByName(instrument);

                int subtractCount = int.Parse(textBox1.Text);
                SubtractFromStoreroom(subtractCount, instrumentId, salesConId);

                string checkQuery = "SELECT payment, dispatch FROM saleinvoices WHERE contractsaleid = @contractsaleid";
                NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@contractsaleid", salesConId);
                NpgsqlDataReader reader = checkCommand.ExecuteReader();

                bool paid = false;
                //bool dispatched = false;
                if (reader.Read())
                {
                    paid = reader.GetBoolean(0);
                    //dispatched = reader.GetBoolean(1);
                }
                reader.Close();

                if (isEnough == true && paid)
                {
                    // Вставить новую запись в таблицу
                    string insertQuery = "INSERT INTO orders (date, amount, summary, contractsaleid, instrumentid) VALUES (@date, @amount, @summary, @contractsaleid, @instrumentid)";
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@date", localTime.ToDateTimeUnspecified());
                    insertCommand.Parameters.AddWithValue("@amount", amount);
                    insertCommand.Parameters.AddWithValue("@summary", total);
                    insertCommand.Parameters.AddWithValue("@contractsaleid", salesConId);
                    insertCommand.Parameters.AddWithValue("@instrumentid", instrumentId);
                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (isEnough == false && paid)
                {
                    MessageBox.Show("Недостаточное количество товара на складе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (isEnough == false && paid == false)
                {
                    string insertQuery = "INSERT INTO orders (date, amount, summary, contractsaleid, instrumentid) VALUES (@date, @amount, @summary, @contractsaleid, @instrumentid)";
                    NpgsqlCommand insertCommand = new NpgsqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@date", localTime.ToDateTimeUnspecified());
                    insertCommand.Parameters.AddWithValue("@amount", amount);
                    insertCommand.Parameters.AddWithValue("@summary", total);
                    insertCommand.Parameters.AddWithValue("@contractsaleid", salesConId);
                    insertCommand.Parameters.AddWithValue("@instrumentid", instrumentId);
                    insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                    DataAdded?.Invoke(this, EventArgs.Empty);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка добавления данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddClientOrder_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            try
            {
                // Заполнение ComboBox из таблицы "publishers"
                string query = "SELECT description FROM contractssale";
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
