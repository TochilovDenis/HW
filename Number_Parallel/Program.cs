using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_Parallel
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxNumber = 10000;

            Console.WriteLine("Начало сравнения методов:");
            // Последовательный поиск простых чисел с обычным For
            PrimeFinderSequential.FindPrimesSequential(maxNumber);
            // Параллельный поиск простых чисел с Parallel.For
            PrimeFinderParallel.FindPrimesParallel(maxNumber);

            Console.WriteLine("Завершено.");
            Console.ReadKey();
        }
    }
}
