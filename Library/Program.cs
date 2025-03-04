using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            //pr.InsertQuery();
            //pr.ReadData();
            //pr.ExecStoredProcedure();

            pr.InsertQueryBooks();
            pr.ReadDataBooks();
            pr.ProcessBooks();
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

        public void ExecStoredProcedure()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("getBooksNumber", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AuthorId", System.Data.
            SqlDbType.Int).Value = 1;
            SqlParameter outputParam = new SqlParameter("@BookCount", System.Data.SqlDbType.Int);
            outputParam.Direction = ParameterDirection.
            Output;
            //outputParam.Value = 0; //заполнять Value не надо!
            cmd.Parameters.Add(outputParam);
            cmd.ExecuteNonQuery();
            Console.WriteLine(cmd.Parameters["@BookCount"].
            Value.ToString());
        }


        // ДЗ
        public void InsertQueryBooks()
        {
            try
            {
                conn.Open();
                string insertString = @"insert into Books (Title, AuthorId, Price, Pages) values (@Title, @AuthorId, @Price, @Pages)";

                SqlCommand cmd = new SqlCommand(insertString, conn);

                cmd.Parameters.AddWithValue("@Title", "C#");
                cmd.Parameters.AddWithValue("@AuthorId", 2);
                cmd.Parameters.AddWithValue("@Price", 604.00);
                cmd.Parameters.AddWithValue("@Pages", 600);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public void ReadDataBooks()
        {
            {
                SqlDataReader rdr = null;
                try
                {
                    //открыть соединение
                    conn.Open();
                    //создать новый объект command с запросом select
                    SqlCommand cmd = new SqlCommand("select * from Books", conn);
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

                        Console.WriteLine("\nОбработано записей: " + rows.Count + "\n");
                    } while (rdr.NextResult());
                }
                finally
                {
                    //закрыть reader
                    if (rdr != null)
                        rdr.Close();
                    //закрыть соединение
                    if (conn != null)
                        conn.Close();
                }
            }
        }

        public void ProcessBooks()
        {
            SqlDataReader rdr = null;
            try
            {
                // Открываем соединение
                conn.Open();

                // Используем ExecuteScalar для получения количества книг
                SqlCommand cmd = new SqlCommand("SELECT COUNT(id) FROM Books", conn);
                int num = (int)cmd.ExecuteScalar();

                // Выполняем основной запрос для получения всех данных
                cmd = new SqlCommand("SELECT * FROM Books", conn);
                rdr = cmd.ExecuteReader();

                // Инициализируем переменные для подсчета сумм
                decimal totalPrice = 0;
                int totalPages = 0;

                // Читаем данные по одной строке
                for (int i = 0; i < num; i++)
                {
                    if (!rdr.Read())
                        break;

                    // Извлекаем значения полей
                    decimal price = rdr.GetDecimal(rdr.GetOrdinal("Price"));
                    int pages = rdr.GetInt32(rdr.GetOrdinal("Pages"));

                    // Суммируем значения
                    totalPrice += price;
                    totalPages += pages;

                    // Выводим информацию о текущей книге
                    Console.WriteLine($"Название: {rdr["Title"]}");
                    Console.WriteLine($"Цена: {price:F2}");
                    Console.WriteLine($"Страниц: {pages}");
                    Console.WriteLine("------------------------");
                }

                // Выводим итоговые суммы
                Console.WriteLine($"\nИтого:");
                Console.WriteLine($"Сумма всех цен: {totalPrice:F2}");
                Console.WriteLine($"Сумма всех страниц: {totalPages}");
            }
            finally
            {
                // Освобождаем ресурсы
                if (rdr != null)
                    rdr.Close();
                if (conn != null)
                    conn.Close();
            }
        }
    } 
}
