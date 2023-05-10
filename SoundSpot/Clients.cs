using Npgsql;
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
    public partial class Clients : UserControl
    {
        public Clients()
        {
            InitializeComponent();
            NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from \"Clients\"";
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                ClientsGridView.DataSource = dataTable;
            }
            cmd.Dispose();
            conn.Close();
        }

        private void Clients_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
