using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundSpot.CustomForms
{
    public partial class EditManufacturer : Form
    {
        public EditManufacturer()
        {
            InitializeComponent();
        }
        public string Manufacturername
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string Manufactureraddress
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public string Manufacturerdirector
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public string Manufacturerbankaccount
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
