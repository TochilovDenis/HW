using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace multithreading_number_generator
{
    // Класс для хранения данных и управления событиями
    public class DataManager
    {
        private int[] numbers;
        private bool isGenerationComplete = false;
        private readonly object locker = new object();
        private readonly ManualResetEventSlim generationComplete = new ManualResetEventSlim(false);

        // Событие, которое срабатывает при завершении генерации
        public event EventHandler GenerationComplete;

        public DataManager()
        {
            numbers = new int[1000];
        }

        // Метод для генерации чисел
        public void GenerateNumbers()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                numbers[i] = random.Next(0, 5001); // 5001 для включения 5000
            }

            lock (locker)
            {
                isGenerationComplete = true;
            }

            generationComplete.Set();
            GenerationComplete.Invoke(this, EventArgs.Empty);
        }

        // Методы для анализа данных
        public int FindMax()
        {
            int max = int.MinValue;
            foreach (var number in numbers)
            {
                if (number > max)
                    max = number;
            }
            return max;
        }

        public int FindMin()
        {
            int min = int.MaxValue;
            foreach (var number in numbers)
            {
                if (number < min)
                    min = number;
            }
            return min;
        }

        public double CalculateAverage()
        {
            long sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }
            return (double)sum / numbers.Length;
        }

        // Метод для ожидания завершения генерации
        public void WaitForGeneration()
        {
            generationComplete.Wait();
        }
    }

    class Program
    {
        static void Main()
        {
            DataManager dataManager = new DataManager();

            // Создаем и запускаем поток для генерации чисел
            Task generationTask = Task.Run(() =>
            {
                Console.WriteLine("Начало генерации чисел...");
                dataManager.GenerateNumbers();
                Console.WriteLine("Генерация завершена!");
            });

            // Создаем потоки для анализа данных
            Task maxTask = Task.Run(() =>
            {
                dataManager.WaitForGeneration();
                int max = dataManager.FindMax();
                Console.WriteLine($"Максимальное значение: {max}");
            });

            Task minTask = Task.Run(() =>
            {
                dataManager.WaitForGeneration();
                int min = dataManager.FindMin();
                Console.WriteLine($"Минимальное значение: {min}");
            });

            Task avgTask = Task.Run(() =>
            {
                dataManager.WaitForGeneration();
                double avg = dataManager.CalculateAverage();
                Console.WriteLine($"Среднее значение: {avg:F2}");
            });

            // Ожидаем завершения всех задач
            Task.WaitAll(generationTask, maxTask, minTask, avgTask);
        }
    }
}
