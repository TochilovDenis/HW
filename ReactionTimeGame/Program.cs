using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionTimeGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Добро пожаловать в игру \"Успел, не успел\"!");
            Console.WriteLine("Когда появится сигнал, нажмите любую клавишу как можно быстрее.");

            Random random = new Random();
            bool playAgain = true;

            while (playAgain)
            {
                Console.WriteLine("\nЖдите сигнал...");

                // Случайная задержка от 1 до 5 секунд
                int delay = random.Next(1000, 5000);
                System.Threading.Thread.Sleep(delay);

                Console.WriteLine("Нажмите любую клавишу!");

                // Запускаем измерение времени
                Stopwatch stopwatch = Stopwatch.StartNew();

                // Ждем нажатия клавиши
                Console.ReadKey(true);

                // Останавливаем измерение
                stopwatch.Stop();

                // Получаем время реакции в миллисекундах
                long reactionTime = stopwatch.ElapsedMilliseconds;

                Console.WriteLine($"\nВаше время реакции: {reactionTime} миллисекунд");

                Console.Write("\nХотите сыграть еще раз? (да/нет): ");
                string answer = Console.ReadLine().ToLower();
                playAgain = answer == "да";
            }

            Console.WriteLine("Спасибо за игру!");
        }
    }
}
