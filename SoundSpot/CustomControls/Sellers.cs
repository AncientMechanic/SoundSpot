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
    public partial class Sellers : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlCommandBuilder builder = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private bool newRowAdd = false;
        public Sellers()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' AS \"Command\" FROM \"Sellers\"", connection);

                builder = new NpgsqlCommandBuilder(dataAdapter);

                dataAdapter.InsertCommand = builder.GetInsertCommand();
                dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                dataAdapter.DeleteCommand = builder.GetDeleteCommand();

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, "\"Sellers\"");

                ClientsGridView.DataSource = dataSet.Tables["\"Sellers\""];

                ClientsGridView.DataBindingComplete += ClientsGridView_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < ClientsGridView.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                ClientsGridView[5, i] = linkCell;
            }
        }

        private void ReloadData()
        {
            try
            {
                dataSet.Tables["\"Sellers\""].Clear();

                dataAdapter.Fill(dataSet, "\"Sellers\"");

                ClientsGridView.DataSource = dataSet.Tables["\"Sellers\""];

                ClientsGridView.DataBindingComplete += ClientsGridView_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            MainMenu mainMenu = parentForm.Controls["mainMenu1"] as MainMenu;
            mainMenu.Visible = true;
        }

        private void Sellers_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ClientsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 5 && ClientsGridView.Rows[e.RowIndex].Cells[5].Value != null)
                {
                    string task = ClientsGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (task == "Delete")
                    {
                        if (ClientsGridView.Columns[e.ColumnIndex].Name == "Command" && e.RowIndex >= 0)
                        {
                            if (MessageBox.Show("You want to DELETE this row?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int id = (int)ClientsGridView.Rows[e.RowIndex].Cells["SellerID"].Value;
                                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM \"Sellers\" WHERE \"SellerID\" = @SellerID", connection))
                                {
                                    cmd.Parameters.AddWithValue("@SellerID", id);
                                    cmd.ExecuteNonQuery();
                                }
                                LoadData(); // Обновляем таблицу

                            }

                        }
                    }
                    else if (task == "Insert")
                    {
                        if (ClientsGridView.Columns[e.ColumnIndex].Name == "Command" && e.RowIndex >= 0)
                        {
                            if (MessageBox.Show("You want to INSERT a new row?", "Insert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int rowIndex = ClientsGridView.Rows.Count - 2;

                                DataRow row = dataSet.Tables["\"Sellers\""].NewRow();

                                row["FirstName"] = ClientsGridView.Rows[rowIndex].Cells["FirstName"].Value;
                                row["LastName"] = ClientsGridView.Rows[rowIndex].Cells["LastName"].Value;
                                row["MiddleName"] = ClientsGridView.Rows[rowIndex].Cells["MiddleName"].Value;
                                row["Salary"] = ClientsGridView.Rows[rowIndex].Cells["Salary"].Value;

                                dataSet.Tables["\"Sellers\""].Rows.Add(row);
                                dataSet.Tables["\"Sellers\""].Rows.RemoveAt(dataSet.Tables["\"Sellers\""].Rows.Count - 2);
                                ClientsGridView.Rows.RemoveAt(ClientsGridView.Rows.Count - 2);
                                ClientsGridView.Rows[e.RowIndex].Cells[5].Value = "Delete";

                                dataAdapter.Update(dataSet, "\"Sellers\"");
                                newRowAdd = false;
                            }
                            else
                            {
                                newRowAdd = false;
                            }
                        }
                    }
                    else if (task == "Update")
                    {
                        if (ClientsGridView.Columns[e.ColumnIndex].Name == "Command" && e.RowIndex >= 0)
                        {
                            if (MessageBox.Show("You want to UPDATE this row?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int r = e.RowIndex;

                                DataRow row = dataSet.Tables["\"Sellers\""].Rows[r];

                                row.BeginEdit();
                                row["FirstName"] = ClientsGridView.Rows[r].Cells["FirstName"].Value;
                                row["LastName"] = ClientsGridView.Rows[r].Cells["LastName"].Value;
                                row["MiddleName"] = ClientsGridView.Rows[r].Cells["MiddleName"].Value;
                                row["Salary"] = ClientsGridView.Rows[r].Cells["Salary"].Value;
                                row.EndEdit();

                                dataAdapter.Update(dataSet, "\"Sellers\"");

                                ClientsGridView.Rows[e.RowIndex].Cells[5].Value = "Delete";
                            }

                        }

                    }

                    ReloadData();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientsGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                if (newRowAdd == false)
                {
                    newRowAdd = true;

                    int lastRow = ClientsGridView.Rows.Count - 2;

                    DataGridViewRow row = ClientsGridView.Rows[lastRow];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    ClientsGridView[5, lastRow] = linkCell;

                    row.Cells["Command"].Value = "Insert";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClientsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (newRowAdd == false)
                {
                    int rowIndex = ClientsGridView.SelectedCells[0].RowIndex;

                    DataGridViewRow editingRow = ClientsGridView.Rows[rowIndex];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    ClientsGridView[5, rowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClientsGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);

            if (ClientsGridView.CurrentCell.ColumnIndex == 4)
            {
                TextBox textBox = e.Control as TextBox;

                if (textBox != null)
                {
                    textBox.KeyPress += new KeyPressEventHandler(Column_KeyPress);
                }
            }
        }
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
