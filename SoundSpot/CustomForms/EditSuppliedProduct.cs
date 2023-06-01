using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundSpot.CustomForms
{
    public partial class EditSuppliedProduct : Form
    {
        public EditSuppliedProduct()
        {
            InitializeComponent();
        }
        public string Instrumentname
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value.ToString(); }
        }
        public decimal Instrumentprice
        {
            get { return decimal.Parse(textBox2.Text); }
            set { textBox2.Text = value.ToString(); }
        }
        public string Instrumenttype
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public string Instrumentmanufacturer
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public string Instrumentsupplier
        {
            get { return textBox5.Text; }
            set { textBox5.Text = value; }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
