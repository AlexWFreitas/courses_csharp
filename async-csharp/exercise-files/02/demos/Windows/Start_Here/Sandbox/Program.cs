using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, int, int> constant = (_, _, x) => x;

            Console.WriteLine(constant(2, 3, 4));
        }
    }
}
