using Microsoft.VisualBasic;
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
    public partial class Manufacturers : UserControl
    {
        private NpgsqlConnection connection = null;
        private NpgsqlCommandBuilder builder = null;
        private NpgsqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private bool newRowAdd = false;
        private string table = "\"Manufacturers\"";
        private string tableID = "ManufacturerID";
        private int numcols = 5;
        public Manufacturers()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form parentForm = this.Parent as Form;
            this.Visible = false;
            MainMenu mainMenu = parentForm.Controls["mainMenu1"] as MainMenu;
            mainMenu.Visible = true;
        }

        private void LoadData()
        {
            try
            {
                dataAdapter = new NpgsqlDataAdapter("SELECT *, 'Delete' AS \"Command\" FROM "+table, connection);

                builder = new NpgsqlCommandBuilder(dataAdapter);

                dataAdapter.InsertCommand = builder.GetInsertCommand();
                dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                dataAdapter.DeleteCommand = builder.GetDeleteCommand();

                dataSet = new DataSet();

                dataAdapter.Fill(dataSet, table);

                ClientsGridView.DataSource = dataSet.Tables[table];

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

                ClientsGridView[numcols, i] = linkCell;
            }
        }

        private void ReloadData()
        {
            try
            {
                dataSet.Tables[table].Clear();

                dataAdapter.Fill(dataSet, table);

                ClientsGridView.DataSource = dataSet.Tables[table];

                ClientsGridView.DataBindingComplete += ClientsGridView_DataBindingComplete;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void Manufacturers_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;Database=SoundSpot;UserId=SoundSpot;Password=Polli1Anna2";
            connection = new NpgsqlConnection(connectionString);
            connection.Open();

            LoadData();
        }
        private int[] GetMaxLengths()
        {
            int[] maxLengths = new int[ClientsGridView.Columns.Count];

            for (int i = 0; i < ClientsGridView.Columns.Count; i++)
            {
                int maxLength = ClientsGridView.Columns[i].HeaderText.Length;
                foreach (DataGridViewRow row in ClientsGridView.Rows)
                {
                    if (row.Cells[i].Value != null)
                    {
                        int cellLength = row.Cells[i].Value.ToString().Length;
                        if (cellLength > maxLength)
                        {
                            maxLength = cellLength;
                        }
                    }
                }
                maxLengths[i] = maxLength;
            }

            return maxLengths;
        }

        private void SavetoFile(string filename)
        {
            FileStream fs = new FileStream(@"C:\AncientMechanic\SoundSpot\reports\" + filename, FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);

            try
            {
                int[] maxLengths = GetMaxLengths();

                for (int j = 0; j < ClientsGridView.Rows.Count; j++)
                {
                    for (int i = 0; i < ClientsGridView.Columns.Count - 1; i++)
                    {
                        string cellValue = (ClientsGridView[i, j].Value ?? "").ToString();

                        string formattedCellValue = string.Format("{0,-" + maxLengths[i] + "}", cellValue);

                        streamWriter.Write(formattedCellValue);
                        if (i < ClientsGridView.Columns.Count - 1)
                        {
                            streamWriter.Write("    ");
                        }
                    }
                    streamWriter.WriteLine();
                }

                streamWriter.Close();
                fs.Close();

                MessageBox.Show("Report saved!");
            }
            catch
            {
                MessageBox.Show("Cannot save report!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = Interaction.InputBox("Save as..", "Save", "Manufacturers.txt");
            SavetoFile(s);
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
                    ClientsGridView[numcols, rowIndex] = linkCell;

                    editingRow.Cells["Command"].Value = "Update";
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

                    ClientsGridView[numcols, lastRow] = linkCell;

                    row.Cells["Command"].Value = "Insert";
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

        private void ClientsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == numcols && ClientsGridView.Rows[e.RowIndex].Cells[numcols].Value != null)
                {
                    string task = ClientsGridView.Rows[e.RowIndex].Cells[numcols].Value.ToString();
                    if (task == "Delete")
                    {
                        if (ClientsGridView.Columns[e.ColumnIndex].Name == "Command" && e.RowIndex >= 0)
                        {
                            if (MessageBox.Show("You want to DELETE this row?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                int id = (int)ClientsGridView.Rows[e.RowIndex].Cells[tableID].Value;
                                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM "+table+" WHERE \""+ tableID + "\" = @"+ tableID, connection))
                                {
                                    cmd.Parameters.AddWithValue("@"+ tableID, id);
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

                                DataRow row = dataSet.Tables[table].NewRow();

                                row["Name"] = ClientsGridView.Rows[rowIndex].Cells["Name"].Value;
                                row["Address"] = ClientsGridView.Rows[rowIndex].Cells["Address"].Value;
                                row["Director"] = ClientsGridView.Rows[rowIndex].Cells["Director"].Value;
                                row["Bank account"] = ClientsGridView.Rows[rowIndex].Cells["Bank account"].Value;

                                dataSet.Tables[table].Rows.Add(row);
                                dataSet.Tables[table].Rows.RemoveAt(dataSet.Tables[table].Rows.Count - 2);
                                ClientsGridView.Rows.RemoveAt(ClientsGridView.Rows.Count - 2);
                                ClientsGridView.Rows[e.RowIndex].Cells[numcols].Value = "Delete";

                                dataAdapter.Update(dataSet, table);
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

                                DataRow row = dataSet.Tables[table].Rows[r];

                                row.BeginEdit();
                                row["Name"] = ClientsGridView.Rows[r].Cells["Name"].Value;
                                row["Address"] = ClientsGridView.Rows[r].Cells["Address"].Value;
                                row["Director"] = ClientsGridView.Rows[r].Cells["Director"].Value;
                                row["Bank account"] = ClientsGridView.Rows[r].Cells["Bank account"].Value;
                                row.EndEdit();

                                dataAdapter.Update(dataSet, table);

                                ClientsGridView.Rows[e.RowIndex].Cells[numcols].Value = "Delete";
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
        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
