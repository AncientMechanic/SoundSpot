using SoundSpot.CustomControls;
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
    public partial class Contracts : UserControl
    {
        public Contracts()
        {
            InitializeComponent();
        }

        private void Contracts_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            MainMenu mainMenu = parentForm.Controls["mainMenu1"] as MainMenu;
            mainMenu.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            SaleContracts salecontracts = parentForm.Controls["saleContracts1"] as SaleContracts;
            salecontracts.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            SupplyContracts supplycontracts = parentForm.Controls["supplyContracts1"] as SupplyContracts;
            supplycontracts.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            SaleInvoices saleInvoices = parentForm.Controls["saleInvoices1"] as SaleInvoices;
            saleInvoices.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            SupplyInvoices supplyInvoices = parentForm.Controls["supplyInvoices1"] as SupplyInvoices;
            supplyInvoices.Visible = true;
        }
    }
}
