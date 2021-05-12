using System;
using static System.Console;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain currentAppDomain = AppDomain.CurrentDomain; 
            currentAppDomain.UnhandledException += 
                new UnhandledExceptionEventHandler(HandleException);

            WriteLine("Enter first number");
            int number1 = int.Parse(ReadLine());

            WriteLine("Enter second number");
            int number2 = int.Parse(ReadLine());

            WriteLine("Enter operator");
            string operation = ReadLine();

            var calculator = new Calculator();
            
            try
            { 
                int result = calculator.Calculate(number1, number2, operation);
                DisplayResult(result);
            }
            catch (ArgumentNullException ex) when (ex.ParamName == "operation")
            {
                // Log.Error(ex)
                WriteLine($"Operation was not provided. \n{ex}");
            }
            catch (ArgumentNullException ex)
            {
                // Log.Error(ex)
                WriteLine($"An argument was null. \n{ex}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Log.Error(ex)
                WriteLine($"Operation is not supported. \n{ex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorry, something went wrong. \n{ex}");
            }
            finally
            {
                WriteLine("...finally...");
            }

            WriteLine("\nPress enter to exit.");
            ReadLine();
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Sorry, there was a problem and we need to close. \nDetails: {e.ExceptionObject}");
        }

        private static void DisplayResult(int result)
        {
            WriteLine($"Result is: {result}");
        }
    }
}
