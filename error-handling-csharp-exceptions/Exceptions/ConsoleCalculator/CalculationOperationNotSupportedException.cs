using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCalculator
{
    class CalculationOperationNotSupportedException : CalculationException
    {
        public string Operation { get; }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with a predefined message.
        /// </summary>
        public CalculationOperationNotSupportedException()
            : base("Specified operation was out of the range of valid values.")
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with the operation that is not supported.
        /// </summary>
        /// <param name="operation"></param>
        public CalculationOperationNotSupportedException(string operation) 
            : this()
        {
            Operation = operation;
        }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with a user-defined message and a wrapped inner exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CalculationOperationNotSupportedException(string message, Exception innerException) 
            : base (message, innerException)
        {
        }

        /// <summary>
        /// Creates a new <see cref="CalculationOperationNotSupportedException"/> with a user-defined message and the name of the operation.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="message"></param>
        public CalculationOperationNotSupportedException(string operation, string message) 
            : base(message)
        {
            Operation = operation;
        }

        public override string Message
        {
            get
            {
                string message = base.Message;

                if (Operation != null)
                {
                    return message + Environment.NewLine + $"Unsupported operation: {Operation}";
                }
                return message; 
            }
        }
    }
}
