using System;
using System.IO;
using System.Windows.Forms;
using NLua;

namespace p4
{
    public partial class Form1 : Form
    {
        double x1, x2;
        int n;

        public Form1()
        {
            InitializeComponent();
        }

        private void CleanOutput()
        {
            dataGridView1.Rows.Clear();
            chart1.Series[0].Points.Clear();
        }

        private void button1_Click(object sender, EventArgs e) //Табулировать
        {
            try
            {
                x1 = Convert.ToDouble(textBox1.Text);
                x2 = Convert.ToDouble(textBox2.Text);
                n = Convert.ToInt32(textBox3.Text);
            }
            catch
            {
                MessageBox.Show("Проверьте правильность ввода.", "Ошибка");
                return;
            }
            if (n <= 0) 
            {
                MessageBox.Show("Количество шагов должно быть больше нуля.", "Ошибка");
                return;
            }
            if (x2 < x1) 
            {
                MessageBox.Show("Конечная точка должна быть больше начальной.", "Ошибка");
                return;
            }

            CleanOutput();

            using (Lua luaState = new Lua()) //новый объект исполнителя Lua
            {
                luaState.DoFile("customscript.lua"); //читаем файл со скриптом
                LuaFunction mainFunc = luaState["main"] as LuaFunction; //Берем из исполнителя нужную функцию
                LuaTable table = mainFunc.Call(x1, x2, n).GetValue(0) as LuaTable; //Вызов возвращает класс LuaTable, внутри которого в Values лежит коллекция
                foreach (LuaTable tableInner in table.Values) // Проходим по значениям
                {
                    double x = (double)tableInner["x"]; //присваивание результатов переменным
                    double y = (double)tableInner["y"];
                    dataGridView1.Rows.Add(x.ToString(), y.ToString()); //добавление резульатов в таблицу
                    chart1.Series[0].Points.AddXY(x, y); //занос результатов в график
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //Очистить
        {
            CleanOutput();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0 && saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] TableLines = new string[dataGridView1.RowCount];
                for (int i = 0; i < TableLines.Length; i++)
                {
                    TableLines[i] = dataGridView1.Rows[i].Cells[0].Value + "\t" + dataGridView1.Rows[i].Cells[1].Value;
                }
                File.WriteAllLines(saveFileDialog1.FileName, TableLines);
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CleanOutput();
                try
                {
                    foreach (string TableLine in File.ReadLines(openFileDialog1.FileName))
                    {
                        string[] LineCells = TableLine.Split('\t');
                        dataGridView1.Rows.Add(LineCells);
                        chart1.Series[0].Points.AddXY(Convert.ToDouble(LineCells[0]), Convert.ToDouble(LineCells[1]));
                    }
                }
                catch
                {
                    CleanOutput();
                    MessageBox.Show("Ошибка при попытке чтения из файла.", "Ошибка");
                }
            }
        }

    }
}
