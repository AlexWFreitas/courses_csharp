using System;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {                       
                                    //  hour, min , sec
            var timeSpan = new TimeSpan( 60 , 100 , 200);

            // 61 hours = 2 days, 13 hours, 43 minutes, 20 seconds.
            Console.WriteLine(timeSpan.Days + " Days");
            Console.WriteLine(timeSpan.Hours + " Hours");
            Console.WriteLine(timeSpan.Minutes + " Minutes");
            Console.WriteLine(timeSpan.Seconds + " Seconds");

            // Other methods - Total Hours
            Console.WriteLine(timeSpan.TotalHours + " total hours.");

            // Other methods - Total Milliseconds
            Console.WriteLine(timeSpan.TotalMilliseconds + " total milliseconds.");


            // Tests with Offset
            var startDifference = DateTimeOffset.UtcNow;
            var endDifference = DateTimeOffset.UtcNow.AddSeconds(45);

            // Data using UtcNow count the miliseconds that it is generated on,
            // so if a command starts a few milliseconds later,
            // then it will have that time difference added

            // The solution is to make both variables point to the same time value.
            var start = DateTimeOffset.UtcNow;
            var end = start.AddSeconds(45);

            TimeSpan difference = end - start;

            // Using operators to apply mathematical operations over a TimeSpan
            difference = difference.Multiply(2);

            Console.WriteLine(difference.TotalMinutes + " minutes.");
        }
    }
}
