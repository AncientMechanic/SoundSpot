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
    public partial class EditSeller : Form
    {
        public EditSeller()
        {
            InitializeComponent();
        }
        public string Sellerfirstname
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string Sellerlastname
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string Sellermiddlename
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public decimal Sellersalary
        {
            get { return decimal.Parse(textBox4.Text); }
            set { textBox4.Text = value.ToString(); }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
