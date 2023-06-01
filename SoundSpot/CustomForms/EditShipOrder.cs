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

namespace SoundSpot.CustomForms
{
    public partial class EditShipOrder : Form
    {
        private NpgsqlConnection? connection = null;
        private decimal total;
        public EditShipOrder()
        {
            InitializeComponent();
        }
        public int OrderName
        {
            get { return int.Parse(textBox5.Text); }
            set { textBox5.Text = value.ToString(); }
        }

        public decimal Amount
        {
            get { return decimal.Parse(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }

        public decimal Sum
        {
            get { return total; }
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
        private void btnOK_Click(object sender, EventArgs e)
        {
            decimal amount = decimal.Parse(textBox1.Text);
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            string query = "SELECT InstrumentID FROM Instruments WHERE Name = @Name";
            NpgsqlCommand command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", textBox4.Text);
            int instrumentId = Convert.ToInt32(command.ExecuteScalar());

            string priceQuery = "SELECT price FROM instruments WHERE InstrumentID = @InstrumentID";
            NpgsqlCommand priceCommand = new NpgsqlCommand(priceQuery, connection);
            priceCommand.Parameters.AddWithValue("@InstrumentID", instrumentId);
            decimal price = Convert.ToDecimal(priceCommand.ExecuteScalar());
            total = amount * price;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
