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
    public partial class Instruments : UserControl
    {
        public Instruments()
        {
            InitializeComponent();
        }

        private void Instruments_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            MainMenu mainMenu = parentForm.Controls["mainMenu1"] as MainMenu;
            mainMenu.Visible = true;
        }


        private void label3_Click_1(object sender, EventArgs e)
        {
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            ClientOrders clientOrders = parentForm.Controls["clientOrders1"] as ClientOrders;
            clientOrders.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            ShipOrders shipOrders = parentForm.Controls["shipOrders1"] as ShipOrders;
            shipOrders.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            Storage storage = parentForm.Controls["storage1"] as Storage;
            storage.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            RSproducts rSproducts = parentForm.Controls["rSproducts1"] as RSproducts;
            rSproducts.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            SuppliedProducts suppliedProducts = parentForm.Controls["suppliedProducts1"] as SuppliedProducts;
            suppliedProducts.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            ProductTypes productType = parentForm.Controls["productTypes1"] as ProductTypes;
            productType.Visible = true;
        }
    }
}
