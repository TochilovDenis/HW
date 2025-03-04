using System;
using System.Threading;
using System.Collections.Generic;

public class CollectionProcessor
{
    private readonly IEnumerable<object> collection;

    public CollectionProcessor(IEnumerable<object> collection)
    {
        this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
    }

    // Метод для обработки коллекции в отдельном потоке
    public void ProcessCollection()
    {
        foreach (var item in collection)
        {
            // Вызываем ToString() для каждого элемента и выводим результат
            Console.WriteLine(item?.ToString() ?? "null");
        }
    }

    // Асинхронный метод для обработки коллекции
    public void ProcessCollectionAsync()
    {
        Thread thread = new Thread(() =>
        {
            try
            {
                ProcessCollection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке коллекции: {ex.Message}");
            }
        });

        thread.Start();
    }
}

class Program
{
    static void Main()
    {
        // Создаем тестовую коллекцию
        List<object> collection = new List<object>
        {
            "Строка",
            42,
            new Person("Иван"),
            DateTime.Now
        };

        // Создаем процессор и запускаем обработку в отдельном потоке
        var processor = new CollectionProcessor(collection);
        processor.ProcessCollectionAsync();

        // Ожидаем завершения работы потока
        Thread.Sleep(1000);
    }
}

// Пример класса с переопределенным ToString()
public class Person
{
    private string name;

    public Person(string name)
    {
        this.name = name;
    }

    public override string ToString()
    {
        return $"Человек: {name}";
    }
}