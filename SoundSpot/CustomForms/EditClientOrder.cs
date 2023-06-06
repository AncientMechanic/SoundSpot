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
using System.Diagnostics.Metrics;
using System.Net;

namespace SoundSpot.CustomForms
{
    public partial class EditClientOrder : Form
    {
        private bool add = false;
        private NpgsqlConnection? connection = null;
        public EditClientOrder()
        {
            InitializeComponent();
        }

        public int OrderName
        {
            get { return int.Parse(textBox5.Text); }
            set { textBox5.Text = value.ToString(); }
        }

        public DateTime Date
        {
            get { return dateTimePicker1.Value; }
            set { dateTimePicker1.Value = value; }
        }

        public decimal Amount
        {
            get { return decimal.Parse(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }

        public decimal Sum
        {
            get { return decimal.Parse(textBox2.Text); }
            set { textBox2.Text = value.ToString(); }
        }

        public string Contract
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }

        public string Instrumentname
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        private int GetSupConIdByName(string saleCon)
        {
            string query = "SELECT contractsaleid FROM contractssale WHERE description = @description";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@description", saleCon);
            int supConId = Convert.ToInt32(command.ExecuteScalar());
            return supConId;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "SELECT instrumentid FROM instruments WHERE name = @name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", textBox4.Text);
            int instrumentId = Convert.ToInt32(command.ExecuteScalar());

            //current amount 
            string selectQuery = "SELECT amount FROM orders WHERE orderid = @orderid";
            NpgsqlCommand selectCommand = new NpgsqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@orderid", OrderName);
            decimal currentCount = (int)selectCommand.ExecuteScalar();

            string countQuery = "SELECT amount FROM storage WHERE instrumentid = @instrumentid";
            NpgsqlCommand countCommand = new NpgsqlCommand(@countQuery, connection);
            countCommand.Parameters.AddWithValue("@instrumentid", instrumentId);
            decimal countStorage = (int)countCommand.ExecuteScalar();

            decimal newAmount = decimal.Parse(textBox1.Text);
            int supConId = GetSupConIdByName(Contract);

            string checkQuery = "SELECT payment, dispatch FROM saleinvoices WHERE contractsaleid = @contractsaleid";
            NpgsqlCommand checkCommand = new NpgsqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@contractsaleid", supConId);
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
                if (add == false)
                {
                    if (newAmount >= currentCount && newAmount - currentCount <= countStorage)
                    {
                        string updateQuery = "UPDATE storage SET amount = amount - (@newAmount - @currentAmount) WHERE  instrumentid = @instrumentid";
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@newAmount", newAmount);
                        updateCommand.Parameters.AddWithValue("@currentAmount", currentCount);
                        updateCommand.Parameters.AddWithValue("@instrumentid", instrumentId);
                        updateCommand.ExecuteNonQuery();
                        add = true;

                    }
                    else if (newAmount < currentCount)
                    {
                        string updateQuery = "UPDATE storage SET amount = amount + (@currentAmount - @newAmount) WHERE  instrumentid = @instrumentid";
                        NpgsqlCommand updateCommand = new NpgsqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@newAmount", newAmount);
                        updateCommand.Parameters.AddWithValue("@currentAmount", currentCount);
                        updateCommand.Parameters.AddWithValue("@instrumentid", instrumentId);
                        updateCommand.ExecuteNonQuery();
                        add = true;

                    }
                    else if (newAmount > countStorage)
                    {
                        MessageBox.Show("Недостаточное количество книг на складе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
            private void EditClientOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
