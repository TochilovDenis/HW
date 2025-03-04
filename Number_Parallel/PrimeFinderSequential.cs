using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_Parallel
{
    // Последовательный поиск простых чисел с обычным For
    class PrimeFinderSequential
    {
        public static void FindPrimesSequential(int maxNumber)
        {
            var sw = Stopwatch.StartNew();

            bool[] isPrime = new bool[maxNumber + 1];
            for (int i = 0; i <= maxNumber; i++)
                isPrime[i] = true;

            for (int x = 2; x <= maxNumber; x++)
            {
                if (isPrime[x])
                {
                    int square = x * x;
                    for (int i = square; i <= maxNumber; i += x)
                        isPrime[i] = false;
                }
            }

            Console.WriteLine($"Последовательный метод выполнился за {sw.ElapsedMilliseconds} мс");
        }
    }
}
