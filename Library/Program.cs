using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        SqlConnection conn = null;
        public Program()
        {
            conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;";
            // или
            //SqlConnection conn = null;
            //conn = new SqlConnection(@"Data Source=(localdb)\v11.0; Initial Catalog=Library; Integrated Security=SSPI;");
        }

        static void Main(string[] args)
        {
            Program pr = new Program();
            pr.InsertQuery();
        }

        // Создание и выполнение запросов (DbCommand)
        public void InsertQuery()
        {
            try
            {
                //открыть соединение
                conn.Open();
                //подготовить запрос insert в переменной типа string
                string insertString = @"insert into Authors (FirstName, LastName) values ('Roger', 'Zelazny')";
                //создать объект command,инициализировав оба свойства
                SqlCommand cmd = new SqlCommand(insertString, conn);
                //выполнить запрос, занесенный в объект command
                cmd.ExecuteNonQuery();  // преднзначен для выполнения запросов insert, update и delete Эт
            }
            finally
            {
                // закрыть соединение
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    } 
}
