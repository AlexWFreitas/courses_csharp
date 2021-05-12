using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter(new Random().Next(10));
            c.ThresholdReached += c_ThresholdReached;

            Console.WriteLine("press 'a' key to increase total");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }

            static void c_ThresholdReached(object sender, EventArgs e)
            {
                Console.WriteLine("The threshold was reached.");
                Environment.Exit(0);
            }

            // Other Code
            /*
            try
            {
                DoSomeUsefulWork();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            */
        }

        private static void DoSomeUsefulWork()
        {
            try
            {
                ICanThrowException();
                ICanThrowException();
            }
            catch (Exception ex)
            {
                Log(ex);
                throw;
            }
        }

        private static void ICanThrowException()
        {
            throw new Exception("Bad thing happened");
        }

        private static void Log(Exception ex)
        {
            // Intentionally left blank
        }
    }
}
