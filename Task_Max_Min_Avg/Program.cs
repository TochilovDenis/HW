using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Max_Min_Avg
{
    class Program
    {
        static void Main(string[] args)
        {
            // Пример массива для обработки
            int[] numbers = { 12, 45, 7, 23, 56, 89, 34 };

            // Создаем массив задач для параллельного выполнения
            Task[] tasks = new Task[4];

            // Задача на поиск минимума
            tasks[0] = Task.Run(() => FindMin(numbers));

            // Задача на поиск максимума
            tasks[1] = Task.Run(() => FindMax(numbers));

            // Задача на вычисление среднего значения
            tasks[2] = Task.Run(() => CalculateAverage(numbers));

            // Задача на вычисление суммы
            tasks[3] = Task.Run(() => CalculateSum(numbers));

            // Ожидаем завершения всех задач
            Task.WaitAll(tasks);

            // Выводим результаты
            Console.WriteLine($"Минимум:          {FindMin(numbers)}");
            Console.WriteLine($"Максимум:         {FindMax(numbers)}");
            Console.WriteLine($"Среднее значение: {CalculateAverage(numbers):F2}");
            Console.WriteLine($"Сумма:            {CalculateSum(numbers)}");
        }

        static int FindMin(int[] array)
        {
            int min = array[0];
            foreach (var number in array)
            {
                if (number < min)
                    min = number;
            }
            return min;
        }

        static int FindMax(int[] array)
        {
            int max = array[0];
            foreach (var number in array)
            {
                if (number > max)
                    max = number;
            }
            return max;
        }

        static double CalculateAverage(int[] array)
        {
            return (double)array.Sum() / array.Length;
        }

        static int CalculateSum(int[] array)
        {
            int sum = 0;
            foreach (var number in array)
            {
                sum += number;
            }
            return sum;
        }
    }
}
