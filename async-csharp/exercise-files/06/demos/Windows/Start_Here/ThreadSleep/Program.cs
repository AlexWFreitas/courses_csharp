using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSleep
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            Task.Run(() =>
            {
                Task.Run(() =>
                {
                    Thread.Sleep(1000);

                    Console.WriteLine("Completed Thread 1");
                });
                Task.Run(() =>
                {
                    Thread.Sleep(2000);

                    Console.WriteLine("Completed Thread 2");
                });
                Task.Run(() =>
                {
                    Thread.Sleep(3000);

                    Console.WriteLine("Completed Thread 3");
                });
            });

            Console.WriteLine("Completed Original Thread");
            Console.ReadLine();
        }
    }
}
