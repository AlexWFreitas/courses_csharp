using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    class CalculationException : Exception
    {
        private static readonly string DefaultMessage = "An error occurred during calculation. Ensure" +
            " that the operator is supported and that the values are within valid ranges for the requested operation.";

        
        public CalculationException() : base(DefaultMessage)
        {
        }

        public CalculationException(string message) : base(message)
        {
        }

        public CalculationException(Exception ex) : base(DefaultMessage, ex)
        {
        }

        public CalculationException(string message, Exception ex) : base(message, ex)
        {
        }


    }
}
