using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.VisualBasic;

namespace SoundSpot
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Clients clients = parentForm.Controls["clients1"] as Clients;
            clients.Visible = true;

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Sellers sellers = parentForm.Controls["sellers1"] as Sellers;
            sellers.Visible = true;
        }
        
        private void button8_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            parentForm.Close();
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Suppliers suppliers = parentForm.Controls["suppliers1"] as Suppliers;
            suppliers.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Manufacturers manufacturers = parentForm.Controls["manufacturers1"] as Manufacturers;
            manufacturers.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Instruments instruments = parentForm.Controls["instruments1"] as Instruments;
            instruments.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //string input = Interaction.InputBox("Prompt", "Title", "Default", 100, 100);
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Contracts contracts = parentForm.Controls["contracts1"] as Contracts;
            contracts.Visible = true;
        }
    }
}
