using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSleep
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting on Original Thread - Sleeping for 250ms after launching other tasks");

            Task.Run(() =>
            {
                Console.WriteLine("Started Task 1 on Thread 1 - Sleeping for 500ms after launching other tasks");

                Task.Run(() =>
                {
                    Console.WriteLine("Started Task 1-1 on Thread 2 - Sleeping for 1000ms");

                    Thread.Sleep(1000);

                    Console.WriteLine("Completed Task 1-1 on Thread 2");
                });
                Task.Run(() =>
                {
                    Console.WriteLine("Started Task 1-2 on Thread 3 - Sleeping for 2000ms");

                    Thread.Sleep(2000);

                    Console.WriteLine("Completed Task 1-2 on Thread 3");
                });
                Task.Run(() =>
                {
                    Console.WriteLine("Started Task 1-3 on Thread 4 - Sleeping for 10000ms");

                    Thread.Sleep(10000);

                    Console.WriteLine("Completed Task 1-3 on Thread 4");
                });

                Thread.Sleep(500);

                Console.WriteLine("Completed Task 1 on Thread 1");
            });

            Thread.Sleep(250);

            Console.WriteLine("Completed Original Thread");
            Console.ReadLine();

            Console.WriteLine("Starting on Original Thread - Sleeping for 5000ms after launching other tasks");

            Task.Run(() =>
            {
                Console.WriteLine("Started Task 1 on Thread 1 - Sleeping for 4000ms after launching other tasks");

                Task.Run(() =>
                {
                    Console.WriteLine("Started Task 1-1 on Thread 2 - Sleeping for 1000ms");

                    Thread.Sleep(1000);

                    Console.WriteLine("Completed Task 1-1 on Thread 2");
                });
                Task.Run(() =>
                {
                    Console.WriteLine("Started Task 1-2 on Thread 3 - Sleeping for 2000ms");

                    Thread.Sleep(2000);

                    Console.WriteLine("Completed Task 1-2 on Thread 3");
                });
                Task.Run(() =>
                {
                    Console.WriteLine("Started Task 1-3 on Thread 4 - Sleeping this Task for 10000ms");

                    Thread.Sleep(10000);

                    Console.WriteLine("Completed Task 1-3 on Thread 4");
                });

                Thread.Sleep(4000);

                Console.WriteLine("Completed Task 1 on Thread 1");
            });

            Thread.Sleep(5000);

            Console.WriteLine("Completed Original Thread");
            Console.ReadLine();






















        }
    }
}
