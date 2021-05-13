using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain currentAppDomain = AppDomain.CurrentDomain; 
            currentAppDomain.UnhandledException += 
                new UnhandledExceptionEventHandler(HandleException);

            Console.WriteLine("Enter first number");
            int number1 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter second number");
            int number2 = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter operator");
            string operation = Console.ReadLine();

            var calculator = new Calculator();
            
            try
            { 
                int result = calculator.Calculate(number1, number2, operation);
                DisplayResult(result);
            }
            catch (CalculationOperationNotSupportedException ex)
            {
                // Log.error(ex);
                Console.WriteLine(ex);
            }
            catch (CalculationException ex)
            {
                // Log.error(ex);
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorry, something went wrong. \n{ex}");
            }
            finally
            {
                Console.WriteLine("...finally...");
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Sorry, there was a problem and we need to close. \nDetails: {e.ExceptionObject}");
        }

        private static void DisplayResult(int result)
        {
            Console.WriteLine($"Result is: {result}");
        }
    }
}
