using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LR5SYAP
{
    internal static class Program
    {
        public static SqlConnection sqlConnection;
        public static void Connect()
        {
            if (sqlConnection == null)
            {
                string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True";
                //string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Иван\source\repos\project1_Parser\Database1.mdf; Integrated Security = True; Connect Timeout = 30";
                //string connectionString = @"Data Source=DESKTOP-NK2VAAA\\SQLEXPRESS;Initial Catalog=C:\USERS\ИВАН\SOURCE\REPOS\PROJECT1_PARSER\DATABASE1.MDF;Integrated Security=true";
                sqlConnection = new SqlConnection(connectionString);
            }

            //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\forgotten\source\repos\project1_Parser\Database1.mdf;Integrated Security=True";

        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
