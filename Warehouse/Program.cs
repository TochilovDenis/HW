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
                Console.WriteLine("3. Показать информацию о товарах");
                Console.WriteLine("4. Выход");

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
                        ShowProductMenu();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        private static void ShowProductMenu()
        {
            while (true)
            {
                Console.WriteLine("\nОперации с товарами:");
                Console.WriteLine("1. Показать все товары");
                Console.WriteLine("2. Показать типы товаров");
                Console.WriteLine("3. Показать поставщиков");
                Console.WriteLine("4. Товар максимального количества");
                Console.WriteLine("5. Товар минимального количества");
                Console.WriteLine("6. Товар минимальной стоимости");
                Console.WriteLine("7. Товар максимальной стоимости");
                Console.WriteLine("8. Показать товары по категории");
                Console.WriteLine("9. Показать товары по поставщику");
                Console.WriteLine("10. Показать самый старый товар");
                Console.WriteLine("11. Показать среднее количество товаров по типам");
                Console.WriteLine("12. Назад в главное меню");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllProducts();
                        break;
                    case "2":
                        ShowProductTypes();
                        break;
                    case "3":
                        ShowSuppliers();
                        break;
                    case "4":
                        ShowMaxQuantityProduct();
                        break;
                    case "5":
                        ShowMinQuantityProduct();
                        break;
                    case "6":
                        ShowMinCostProduct();
                        break;
                    case "7":
                        ShowMaxCostProduct();
                        break;
                    case "8":
                        ShowProductsByCategory();
                        break;
                    case "9":
                        ShowProductsBySupplier();
                        break;
                    case "10":
                        ShowOldestProduct();
                        break;
                    case "11":
                        ShowAverageQuantityByType();
                        break;
                    case "12":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        private static void ShowAllProducts()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            g.GoodName, 
                            g.CostPrice, 
                            s.Quantity, 
                            s.GoodType, 
                            p.ProviderName, 
                            g.DeliveryDate
                        FROM Goods g
                        JOIN Storage s ON g.GoodID = s.GoodID
                        LEFT JOIN Providers p ON g.ProviderID = p.ProviderID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("\nСписок всех товаров:");
                            Console.WriteLine("Название | Себестоимость | Количество | Тип | Поставщик | Дата поставки");
                            Console.WriteLine(new string('-', 80));

                            while (reader.Read())
                            {
                                string providerName = reader.IsDBNull(reader.GetOrdinal("ProviderName")) ?
                                    "Нет поставщика" : reader.GetString(reader.GetOrdinal("ProviderName"));

                                Console.WriteLine($"{reader.GetString(0)} | {reader.GetDecimal(1):F2} | " +
                                    $"{reader.GetInt32(2)} | {reader.GetString(3)} | {providerName} | " +
                                    $"{reader.GetDateTime(5):dd.MM.yyyy}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowProductTypes()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT DISTINCT GoodType FROM Storage";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nСписок типов товаров:");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int count = 1;
                            while (reader.Read())
                            {
                                Console.WriteLine($"{count}. {reader.GetString(0)}");
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowSuppliers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT ProviderName FROM Providers";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nСписок поставщиков:");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int count = 1;
                            while (reader.Read())
                            {
                                Console.WriteLine($"{count}. {reader.GetString(0)}");
                                count++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowMaxQuantityProduct()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT TOP 1 
                            g.GoodName, 
                            s.Quantity, 
                            s.GoodType,
                            g.CostPrice
                        FROM Storage s
                        JOIN Goods g ON s.GoodID = g.GoodID
                        ORDER BY s.Quantity DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nТовар максимального количества:");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Название: {reader.GetString(0)}");
                                Console.WriteLine($"Количество: {reader.GetInt32(1)}");
                                Console.WriteLine($"Тип: {reader.GetString(2)}");
                                Console.WriteLine($"Себестоимость: {reader.GetDecimal(3):F2}");
                            }
                            else
                            {
                                Console.WriteLine("Данные не найдены");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowMinQuantityProduct()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT TOP 1 
                            g.GoodName, 
                            s.Quantity, 
                            s.GoodType,
                            g.CostPrice
                        FROM Storage s
                        JOIN Goods g ON s.GoodID = g.GoodID
                        ORDER BY s.Quantity ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nТовар минимального количества:");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Название: {reader.GetString(0)}");
                                Console.WriteLine($"Количество: {reader.GetInt32(1)}");
                                Console.WriteLine($"Тип: {reader.GetString(2)}");
                                Console.WriteLine($"Себестоимость: {reader.GetDecimal(3):F2}");
                            }
                            else
                            {
                                Console.WriteLine("Данные не найдены");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowMinCostProduct()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT TOP 1 
                            g.GoodName, 
                            g.CostPrice,
                            s.Quantity,
                            s.GoodType
                        FROM Goods g
                        JOIN Storage s ON g.GoodID = s.GoodID
                        ORDER BY g.CostPrice ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nТовар минимальной стоимости:");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Название: {reader.GetString(0)}");
                                Console.WriteLine($"Себестоимость: {reader.GetDecimal(1):F2}");
                                Console.WriteLine($"Количество: {reader.GetInt32(2)}");
                                Console.WriteLine($"Тип: {reader.GetString(3)}");
                            }
                            else
                            {
                                Console.WriteLine("Данные не найдены");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowMaxCostProduct()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT TOP 1 
                            g.GoodName, 
                            g.CostPrice,
                            s.Quantity,
                            s.GoodType
                        FROM Goods g
                        JOIN Storage s ON g.GoodID = s.GoodID
                        ORDER BY g.CostPrice DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nТовар максимальной стоимости:");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Console.WriteLine($"Название: {reader.GetString(0)}");
                                Console.WriteLine($"Себестоимость: {reader.GetDecimal(1):F2}");
                                Console.WriteLine($"Количество: {reader.GetInt32(2)}");
                                Console.WriteLine($"Тип: {reader.GetString(3)}");
                            }
                            else
                            {
                                Console.WriteLine("Данные не найдены");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowProductsByCategory()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Console.WriteLine("\nВыберите тип товара:");
                    ShowProductTypes();

                    string category = Console.ReadLine();
                    int categoryNumber;
                    if (!int.TryParse(category, out categoryNumber))
                    {
                        Console.WriteLine("Неверный формат ввода!");
                        return;
                    }

                    string query = @"
                SELECT 
                    g.GoodName, 
                    g.CostPrice, 
                    s.Quantity, 
                    s.GoodType, 
                    p.ProviderName, 
                    g.DeliveryDate
                FROM Goods g
                JOIN Storage s ON g.GoodID = s.GoodID
                LEFT JOIN Providers p ON g.ProviderID = p.ProviderID
                WHERE s.GoodType = (
                    SELECT TOP 1 GoodType 
                    FROM Storage 
                    ORDER BY GoodType 
                    OFFSET @CategoryNumber - 1
                )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryNumber", categoryNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("\nТовары выбранной категории:");
                            Console.WriteLine("Название | Себестоимость | Количество | Тип | Поставщик | Дата поставки");
                            Console.WriteLine(new string('-', 80));

                            while (reader.Read())
                            {
                                string providerName = reader.IsDBNull(reader.GetOrdinal("ProviderName")) ?
                                    "Нет поставщика" : reader.GetString(reader.GetOrdinal("ProviderName"));

                                Console.WriteLine($"{reader.GetString(0)} | {reader.GetDecimal(1):F2} | " +
                                    $"{reader.GetInt32(2)} | {reader.GetString(3)} | {providerName} | " +
                                    $"{reader.GetDateTime(5):dd.MM.yyyy}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowProductsBySupplier()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Console.WriteLine("\nВыберите поставщика:");
                    ShowSuppliers();

                    string supplier = Console.ReadLine();
                    int supplierNumber;
                    if (!int.TryParse(supplier, out supplierNumber))
                    {
                        Console.WriteLine("Неверный формат ввода!");
                        return;
                    }

                    string query = @"
                SELECT 
                    g.GoodName, 
                    g.CostPrice, 
                    s.Quantity, 
                    s.GoodType, 
                    p.ProviderName, 
                    g.DeliveryDate
                FROM Goods g
                JOIN Storage s ON g.GoodID = s.GoodID
                JOIN Providers p ON g.ProviderID = p.ProviderID
                WHERE p.ProviderID = (
                    SELECT TOP 1 ProviderID 
                    FROM Providers 
                    ORDER BY ProviderID 
                    OFFSET @SupplierNumber - 1
                )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierNumber", supplierNumber);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("\nТовары выбранного поставщика:");
                            Console.WriteLine("Название | Себестоимость | Количество | Тип | Поставщик | Дата поставки");
                            Console.WriteLine(new string('-', 80));

                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetString(0)} | {reader.GetDecimal(1):F2} | " +
                                    $"{reader.GetInt32(2)} | {reader.GetString(3)} | {reader.GetString(4)} | " +
                                    $"{reader.GetDateTime(5):dd.MM.yyyy}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowOldestProduct()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT TOP 1 
                    g.GoodName, 
                    g.DeliveryDate,
                    s.Quantity,
                    s.GoodType,
                    p.ProviderName,
                    g.CostPrice
                FROM Goods g
                JOIN Storage s ON g.GoodID = s.GoodID
                LEFT JOIN Providers p ON g.ProviderID = p.ProviderID
                ORDER BY g.DeliveryDate ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nСамый старый товар на складе:");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string providerName = reader.IsDBNull(reader.GetOrdinal("ProviderName")) ?
                                    "Нет поставщика" : reader.GetString(reader.GetOrdinal("ProviderName"));

                                Console.WriteLine($"Название: {reader.GetString(0)}");
                                Console.WriteLine($"Дата поставки: {reader.GetDateTime(1):dd.MM.yyyy}");
                                Console.WriteLine($"Количество: {reader.GetInt32(2)}");
                                Console.WriteLine($"Тип: {reader.GetString(3)}");
                                Console.WriteLine($"Поставщик: {providerName}");
                                Console.WriteLine($"Себестоимость: {reader.GetDecimal(5):F2}");
                            }
                            else
                            {
                                Console.WriteLine("Данные не найдены");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }
        }

        private static void ShowAverageQuantityByType()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    s.GoodType,
                    AVG(CAST(s.Quantity AS DECIMAL(10,2))) as AverageQuantity
                FROM Storage s
                GROUP BY s.GoodType
                ORDER BY s.GoodType";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        Console.WriteLine("\nСреднее количество товаров по типам:");
                        Console.WriteLine("Тип товара | Среднее количество");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader.GetString(0)} | {reader.GetDecimal(1):F2}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
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
