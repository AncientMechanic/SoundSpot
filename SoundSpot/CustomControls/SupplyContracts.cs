using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoundSpot
{
    public partial class SupplyContracts : UserControl
    {
        public SupplyContracts()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Contracts contracts = parentForm.Controls["contracts1"] as Contracts;
            contracts.Visible = true;
        }
    }
}
