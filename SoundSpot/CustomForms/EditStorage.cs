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
    public partial class EditStorage : Form
    {
        public EditStorage()
        {
            InitializeComponent();
        }
        public decimal StorageAmount
        {
            get { return decimal.Parse(textBox1.Text); }
            set { textBox1.Text = value.ToString(); }
        }
        public string StorageInstrument
        {
            get { return comboBox1.Text; }
            set { comboBox1.Text = value; }
        }



        private void EditStorage_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
