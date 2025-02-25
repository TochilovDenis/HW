using System;
using System.Collections.Generic;
using System.Configuration;
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
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
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
                // Получаем имена полей и их длины
                int[] columnWidths = new int[rdr.FieldCount];
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    columnWidths[i] = rdr.GetName(i).Length;
                }

                // Читаем данные и обновляем максимальные длины
                List<string[]> rows = new List<string[]>();
                do
                {
                    while (rdr.Read())
                    {
                        string[] row = new string[rdr.FieldCount];
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            row[i] = rdr[i].ToString();
                            columnWidths[i] = Math.Max(columnWidths[i], row[i].Length);
                        }
                        rows.Add(row);
                    }

                    // Выводим шапку
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.Write(rdr.GetName(i).PadRight(columnWidths[i] + 2));
                    }
                    Console.WriteLine();

                    // Выводим разделитель
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.Write(new string('-', columnWidths[i] + 2));
                    }
                    Console.WriteLine();

                    // Выводим данные
                    foreach (string[] row in rows)
                    {
                        for (int i = 0; i < row.Length; i++)
                        {
                            Console.Write(row[i].PadRight(columnWidths[i] + 2));
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine("\nОбработано записей: " + rows.Count);
                } while (rdr.NextResult());
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
