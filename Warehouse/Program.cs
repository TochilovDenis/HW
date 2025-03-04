using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    class Program
    {
        private static readonly string connectionString;

        static Program()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Приложение для работы со складской базой данных");

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Подключиться к базе данных");
                Console.WriteLine("2. Отключиться от базы данных");
                Console.WriteLine("3. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ConnectToDatabase();
                        break;
                    case "2":
                        DisconnectFromDatabase();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        private static void ConnectToDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Успешно подключено к базе данных!");

                    // Проверяем наличие таблиц
                    using (SqlCommand command = new SqlCommand(
                        @"SELECT COUNT(*) FROM sys.tables WHERE name IN ('Providers', 'Goods', 'Storage')",
                        connection))
                    {
                        int tablesCount = (int)command.ExecuteScalar();

                        if (tablesCount != 3)
                        {
                            CreateTables(connection);
                            Console.WriteLine("Таблицы созданы успешно.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при подключении к базе данных: {ex.Message}");
            }
        }

        private static void DisconnectFromDatabase()
        {
            Console.WriteLine("Отключение от базы данных...");

            // В .NET подключения автоматически возвращаются в пул после закрытия
            Console.WriteLine("Успешно отключено от базы данных.");
        }

        private static void CreateTables(SqlConnection connection)
        {
            string[] createTableQueries = new string[]
            {
                @"CREATE TABLE IF NOT EXISTS Providers (
                    ProviderID INT PRIMARY KEY IDENTITY(1,1),
                    ProviderName NVARCHAR(100) NOT NULL,
                    ContactInfo NVARCHAR(200)
                )",

                @"CREATE TABLE IF NOT EXISTS Goods (
                    GoodID INT PRIMARY KEY IDENTITY(1,1),
                    GoodName NVARCHAR(100) NOT NULL,
                    ProviderID INT,
                    CostPrice DECIMAL(10,2),
                    DeliveryDate DATETIME,
                    CONSTRAINT FK_Goods_Providers FOREIGN KEY (ProviderID) 
                    REFERENCES Providers(ProviderID)
                )",

                @"CREATE TABLE IF NOT EXISTS Storage (
                    StorageID INT PRIMARY KEY IDENTITY(1,1),
                    GoodID INT,
                    Quantity INT,
                    GoodType NVARCHAR(50),
                    CONSTRAINT FK_Storage_Goods FOREIGN KEY (GoodID) 
                    REFERENCES Goods(GoodID)
                )"
            };

            foreach (string query in createTableQueries)
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
