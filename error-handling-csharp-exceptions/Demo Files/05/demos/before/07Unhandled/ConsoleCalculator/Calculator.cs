﻿using System;

namespace ConsoleCalculator
{
    public class Calculator
    {
        public int Calculate(int number1, int number2, string operation)
        {
            throw new ArgumentNullException(nameof(number1));

            string nonNullOperation = 
                operation ?? throw new ArgumentNullException(nameof(operation));

            //if (operation is null)
            //{
            //    throw new ArgumentNullException(nameof(operation));
            //}

            if (nonNullOperation == "/")
            {
                try
                {
                    return Divide(number1, number2);
                }
                catch (DivideByZeroException ex)
                {
                    // Log.Error(ex);
                    //throw;

                    throw new ArithmeticException("An error occurred during calculation.",
                        ex);
                }

            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(operation),
                    "The mathematical operator is not supported.");

                //Console.WriteLine("Unknown operation.");
                //return 0;
            }
        }
        
        private int Divide(int number, int divisor)
        {
            return number / divisor;
        }
    }
}

