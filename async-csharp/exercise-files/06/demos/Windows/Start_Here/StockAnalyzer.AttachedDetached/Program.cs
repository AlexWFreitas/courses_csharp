using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.AttachedDetached
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting");

            // Examples showing a few concepts
            // async anonymous methods can return the Task.Result, but the returned value is always a Task.
            // You generally return the Task.Result instead of a Task inside of a Task delegate.
            // If you do return a Task instead of Task.Result from a Task method code, ... ...
            // Task.Run unwraps the Task of Task of Result into Task of Result automatically for you
            // Task.Factory.StartNew doesn't do this though.

            Task<Task<string>> Task1 = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(2000);

                return "PluralSight!";
            });

            Task<string> UnwrappedTask = Task.Factory.StartNew(async () =>
            {
                await Task.Delay(2000);

                return "PluralSight!";
            }).Unwrap(); // Unwrap method

            Task<Task<string>> Task3a = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);

                return Task.FromResult("PluralSight!");
            });

            Task<string> Task3 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);

                return "PluralSight!";
            });

            Task<string> Task2 = Task.Run(async () =>
            {
                await Task.Delay(2000);

                return "PluralSight!";
            });

            Task<string> Task4 = Task.Run(() =>
            {
                Thread.Sleep(2000);

                return "PluralSight!";
            });

            Task<string> Task4a = Task.Run(() =>
            {
                Thread.Sleep(2000);

                return Task.FromResult("PluralSight!");
            });

            Console.WriteLine("Completed");
            Console.ReadLine();
        }
    }
}
