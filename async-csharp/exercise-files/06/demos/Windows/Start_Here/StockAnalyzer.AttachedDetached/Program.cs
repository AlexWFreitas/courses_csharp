using System;
using System.Threading;
using System.Threading.Tasks;

namespace StockAnalyzer.AttachedDetached
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Example related to awaited on a chain of Tasks whose results depend from the nested Tasks operations.
            var x = await Run();
            Console.WriteLine(x);


            // Example related to Independent Nested Tasks being attached to the awaited Parent Task.
            Console.WriteLine("Starting");

            await Task.Factory.StartNew(() =>
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(1000);

                    Console.WriteLine("Completed 1");
                }, TaskCreationOptions.AttachedToParent);
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(2000);

                    Console.WriteLine("Completed 2");
                }, TaskCreationOptions.AttachedToParent);
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(3000);

                    Console.WriteLine("Completed 3");
                }, TaskCreationOptions.AttachedToParent);
            });
            
            Console.WriteLine("Completed");
            Console.ReadLine();
        }

        public static Task<string> Run()
        {
            return Task.Run(() => Compute());
        }

        public static Task<string> Compute()
        {
            Thread.Sleep(5000);
            return Task.Run(() => Load());
        }

        public static Task<string> Load()
        {
            return Task.Run(() => "Even though I have awaited the top of the chain, it still properly validated every nested Task.");
        }
    }
}
