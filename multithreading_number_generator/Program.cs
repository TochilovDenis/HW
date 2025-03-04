using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace multithreading_number_generator
{
    class Program
    {
        // Массив для хранения чисел
        private static int[] numbers = new int[1000];

        // Событие для сигнала о завершении генерации
        private static event EventHandler GenerationComplete;

        // Мьютекс для синхронизации доступа к общим ресурсам
        private static readonly object locker = new object();

        // Флаг завершения генерации
        private static bool isGenerationComplete = false;

        static void Main()
        {
            // Создаем поток для генерации чисел
            Thread generationThread = new Thread(GenerateNumbers);

            // Создаем три потока для анализа данных
            Thread maxThread = new Thread(FindMax);
            Thread minThread = new Thread(FindMin);
            Thread avgThread = new Thread(CalculateAverage);

            // Подписываемся на событие завершения генерации
            GenerationComplete += (sender, e) =>
            {
                Console.WriteLine("Генерация завершена. Начинаем анализ данных...");
                maxThread.Start();
                minThread.Start();
                avgThread.Start();
            };

            // Запускаем поток генерации
            Console.WriteLine("Начало генерации чисел...");
            generationThread.Start();

            // Ожидаем завершения всех потоков
            generationThread.Join();
            maxThread.Join();
            minThread.Join();
            avgThread.Join();

            Console.WriteLine("Все операции завершены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // Метод для генерации чисел
        static void GenerateNumbers()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                numbers[i] = random.Next(0, 5001);
            }

            lock (locker)
            {
                isGenerationComplete = true;
            }

            // Вызываем событие о завершении генерации
            GenerationComplete?.Invoke(null, EventArgs.Empty);
        }

        // Метод для поиска максимума
        static void FindMax()
        {
            int max = int.MinValue;
            foreach (var number in numbers)
            {
                if (number > max)
                    max = number;
            }
            Console.WriteLine($"Максимальное значение: {max}");
        }

        // Метод для поиска минимума
        static void FindMin()
        {
            int min = int.MaxValue;
            foreach (var number in numbers)
            {
                if (number < min)
                    min = number;
            }
            Console.WriteLine($"Минимальное значение: {min}");
        }

        // Метод для вычисления среднего значения
        static void CalculateAverage()
        {
            long sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }
            double average = (double)sum / numbers.Length;
            Console.WriteLine($"Среднее значение: {average:F2}");
        }
    }
}
