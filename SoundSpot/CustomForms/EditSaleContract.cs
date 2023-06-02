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
        private void EditSaleContract_Load(object sender, EventArgs e)
        {

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
