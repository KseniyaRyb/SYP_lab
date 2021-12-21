using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LR5SYAP
{
    public partial class Form1 : Form
    {
        bool loaded = false;
        public Form1()
        {
            InitializeComponent();
            UpdateTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (dataGridView1.CurrentCell.ColumnIndex)
            {
                case 9:
                    Form3 form3 = new Form3(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
                    Form3 f3 = form3;
                    f3.Owner = this;
                    f3.Show();

                    break;
                case 10:
                    string commandID = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    Program.sqlConnection.Open();
                    DeleteCommand(commandID);
                    UpdateTable();
                    break;
            }
            
        }

        public async void UpdateTable()
        {
            Program.Connect();
            await Program.sqlConnection.OpenAsync();

            SqlCommand commandSelect = new SqlCommand("SELECT * FROM [CommandEntities]", Program.sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                dataGridView1.Rows.Clear();
                sqlReader = await commandSelect.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    dataGridView1.Rows.Add(Convert.ToString(sqlReader["commandID"]), Convert.ToString(sqlReader["commandTrigger"]), Convert.ToString(sqlReader["sourceNames"]), Convert.ToString(sqlReader["commandAnswer"]), Convert.ToString(sqlReader["isScript"]), Convert.ToString(sqlReader["scriptName"]), Convert.ToString(sqlReader["date"]), Convert.ToString(sqlReader["description"]), Convert.ToString(sqlReader["autor"]));
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                Program.sqlConnection.Close();
                loaded = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Form2 f2 = form2;
            f2.Owner = this;
            f2.Show();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if(loaded) UpdateTable();
        }

        void DeleteCommand(string commandID)
        {
            SqlCommand commandSelect = new SqlCommand($"DELETE FROM [CommandEntities] WHERE [commandID] = N'{commandID}'", Program.sqlConnection);
            commandSelect.Parameters.AddWithValue("commandID", commandID);
            SqlDataReader sqlReader = null;
            try
            {
                sqlReader = commandSelect.ExecuteReader();

                while (sqlReader.Read())
                {
                    commandSelect.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                Program.sqlConnection.Close();
            }
        }
    }
}
