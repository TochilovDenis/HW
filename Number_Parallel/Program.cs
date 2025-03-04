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
            PrimeFinderParallel.FindPrimesParallel(maxNumber);

            Console.WriteLine("Завершено.");
            Console.ReadKey();
        }
    }
}
