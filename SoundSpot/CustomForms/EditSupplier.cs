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
    public partial class EditSupplier : Form
    {
        public EditSupplier()
        {
            InitializeComponent();
        }
        public string Suppliername
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }
        public string Supplieraddress
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
