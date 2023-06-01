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
    public partial class EditClient : Form
    {
        public EditClient()
        {
            InitializeComponent();
        }
        public string Clientsfirstname
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string Clientslastname
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        public string Clientsmiddlename
        {
            get { return textBox3.Text; }
            set { textBox3.Text = value; }
        }
        public string Clientsaddress
        {
            get { return textBox4.Text; }
            set { textBox4.Text = value; }
        }
        public string Clientsbankaccount
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
