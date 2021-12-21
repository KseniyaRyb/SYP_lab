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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.sqlConnection.Open();

            string commandTrigger = dataGridView1.Rows[0].Cells[0].Value.ToString(),
                sourceNames = dataGridView1.Rows[0].Cells[1].Value.ToString(),
                commandAnswer = dataGridView1.Rows[0].Cells[2].Value.ToString(),
                isScript = dataGridView1.Rows[0].Cells[3].Value.ToString(),
                scriptName = dataGridView1.Rows[0].Cells[4].Value.ToString(),
                date = dataGridView1.Rows[0].Cells[5].Value.ToString(),
                description = dataGridView1.Rows[0].Cells[6].Value.ToString(),
                autor = dataGridView1.Rows[0].Cells[7].Value.ToString();
            TableAdd(commandTrigger, sourceNames, commandAnswer, isScript, scriptName, date, description, autor);

        }

        void TableAdd(string commandTrigger, string sourceNames, string commandAnswer, string isScript, string scriptName, string date, string description, string autor)
        {
            SqlCommand command2 = new SqlCommand($"INSERT [CommandEntities]([commandTrigger], [sourceNames], [commandAnswer], [isScript], [scriptName], [date], [description], [autor]) VALUES (N'{commandTrigger}', N'{sourceNames}', N'{commandAnswer}', N'{isScript}', N'{scriptName}', N'{date}', N'{description}', N'{autor}')", Program.sqlConnection);
            command2.Parameters.AddWithValue("commandTrigger", commandTrigger);
            command2.Parameters.AddWithValue("sourceNames", sourceNames); 
            command2.Parameters.AddWithValue("commandAnswer", commandAnswer);
            command2.Parameters.AddWithValue("isScript", isScript); 
            command2.Parameters.AddWithValue("scriptName", scriptName);
            command2.Parameters.AddWithValue("date", date); 
            command2.Parameters.AddWithValue("description", description);
            command2.Parameters.AddWithValue("autor", autor);

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
