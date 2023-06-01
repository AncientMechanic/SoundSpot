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
    public partial class EditClientOrder : Form
    {
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
