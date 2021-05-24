using System;
using System.Threading.Tasks;

namespace SandBox
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(await SemAsync(3));
            Console.WriteLine(await ComAsync(3));
        }

        static Task<int> SemAsync(int n)
        {
            var task = Task.Run(() => n);
            return task;
        }

        static async Task<int> ComAsync(int n)
        {
            var task = Task.Run(() => n);
            await task;
            return task.Result;
        }
    }
}
