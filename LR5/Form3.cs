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
    public partial class Form3 : Form
    {
        public Form3(string commandID)
        {
            InitializeComponent();
            SelectIDTable(commandID);
        }

        public async void SelectIDTable(string commandID)
        {
            Program.Connect();
            await Program.sqlConnection.OpenAsync();

            SqlCommand commandSelect = new SqlCommand($"SELECT * FROM [CommandEntities] WHERE [commandID] = N'{commandID}'", Program.sqlConnection);
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.sqlConnection.Open();

            string commandTrigger = dataGridView2.Rows[0].Cells[0].Value.ToString(),
                sourceNames = dataGridView2.Rows[0].Cells[1].Value.ToString(),
                commandAnswer = dataGridView2.Rows[0].Cells[2].Value.ToString(),
                isScript = dataGridView2.Rows[0].Cells[3].Value.ToString(),
                scriptName = dataGridView2.Rows[0].Cells[4].Value.ToString(),
                date = dataGridView2.Rows[0].Cells[5].Value.ToString(),
                description = dataGridView2.Rows[0].Cells[6].Value.ToString(),
                autor = dataGridView2.Rows[0].Cells[7].Value.ToString(),
                commandID = dataGridView1.Rows[0].Cells[0].Value.ToString();
            TableUpdate(commandID, commandTrigger, sourceNames, commandAnswer, isScript, scriptName, date, description, autor);
        }

        void TableUpdate(string commandID, string commandTrigger, string sourceNames, string commandAnswer, string isScript, string scriptName, string date, string description, string autor)
        {
            SqlCommand command2 = new SqlCommand($"UPDATE [CommandEntities]" +
                $"SET [commandTrigger] = N'{commandTrigger}', " +
                $"[sourceNames] = N'{sourceNames}', " +
                $"[commandAnswer] = N'{commandAnswer}', " +
                $"[isScript] = N'{isScript}', " +
                $"[scriptName] = N'{scriptName}', " +
                $"[date] = N'{date}', " +
                $"[description] = N'{description}', " +
                $"[autor] = N'{autor}' " +
                $"WHERE [commandID] = N'{commandID}'", Program.sqlConnection);

            command2.Parameters.AddWithValue("commandTrigger", commandTrigger);
            command2.Parameters.AddWithValue("sourceNames", sourceNames);
            command2.Parameters.AddWithValue("commandAnswer", commandAnswer);
            command2.Parameters.AddWithValue("isScript", isScript);
            command2.Parameters.AddWithValue("scriptName", scriptName);
            command2.Parameters.AddWithValue("date", date);
            command2.Parameters.AddWithValue("description", description);
            command2.Parameters.AddWithValue("autor", autor);
            command2.Parameters.AddWithValue("commandID", commandID);

            try
            {
                command2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Program.sqlConnection.Close();
            }
        }
    }
}
