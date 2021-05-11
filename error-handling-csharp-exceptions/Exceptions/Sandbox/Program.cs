using System;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DoSomeUsefulWork();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
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
