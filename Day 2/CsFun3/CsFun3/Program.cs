// See https://aka.ms/new-console-template for more information

namespace CsFun3;

static class Program
{
        static async Task Main(string[] args)
        {
                //return prime numbers for a range (e.g. from 0 to 100)
                int start = 0;
                int end = 100;

                List<int> result = await PrimeNumber.GetPrimesAsync(start, end);
            
                Console.WriteLine("Prime numbers between {0} and {1}:", start, end );
                foreach (int prime in result)
                {
                        Console.Write(prime + " ");
                        // Thread.Sleep(1000);
                }
        }
}

