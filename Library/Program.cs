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
            pr.ReadData();
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

        // Получение и обработка результатов запросов(DbDataReader)
        public void ReadData()
        {
            SqlDataReader rdr = null;
            try
            {
                //открыть соединение
                conn.Open();
                //создать новый объект command с запросом select
                SqlCommand cmd = new SqlCommand("select * from Authors", conn);
                //выполнить запрос select, сохранив возвращенный результат
                rdr = cmd.ExecuteReader();
                int line = 0; // счетчик строк
                //извлечь полученные строки
                while (rdr.Read())
                {
                    //формируем шапку таблицы перед выводом первой строки
                    if (line == 0)
                    {
                        //цикл по числу прочитанных полей
                        for(int i = 0; i < rdr.FieldCount; i++)
                        {
                            //вывести в консольное окно имена полей
                            Console.Write(rdr.GetName(i).ToString() + " ");
                        }
                    }
                    line++;
                    Console.WriteLine("\n" + rdr[0] + (" ") + rdr[1] + " " + rdr[2]);
                }
                Console.WriteLine("\nОбработано записей: " + line.ToString());
            }
            finally
            {
                //закрыть reader
                if (rdr != null)
                {
                    rdr.Close();
                }
                //закрыть соединение
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    } 
}
