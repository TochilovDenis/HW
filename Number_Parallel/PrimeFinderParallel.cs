using System;
using System.Diagnostics;

using System.Threading.Tasks;

namespace Number_Parallel
{
    class PrimeFinderParallel
    {
        public static void FindPrimesParallel(int maxNumber)
        {
            var sw = Stopwatch.StartNew();

            bool[] isPrime = new bool[maxNumber + 1];
            for (int i = 0; i <= maxNumber; i++)
                isPrime[i] = true;

            Parallel.For(2, maxNumber + 1, x =>
            {
                if (isPrime[x])
                {
                    int square = x * x;
                    for (int i = square; i <= maxNumber; i += x)
                        isPrime[i] = false;
                }
            });

            Console.WriteLine($"Параллельный метод выполнился за {sw.ElapsedMilliseconds} мс");
        }
    }
}
