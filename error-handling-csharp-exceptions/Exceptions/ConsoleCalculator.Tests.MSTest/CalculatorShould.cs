using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleCalculator.Tests.MSTest
{
    [TestClass]
    public class CalculatorShould
    {
        [TestMethod]
        public void ThrowWhenUnsupportedOperation()
        {
            // Arrange
            // sut = system under test
            var sut = new Calculator();

            // Act and Assert
            Assert.ThrowsException<CalculationOperationNotSupportedException>(
                () => sut.Calculate(2, 4, "+") );

            // Needs to be the exception to be the same - So this line will fail.

            // Assert.ThrowsException<CalculationException>(
            //    () => sut.Calculate(2, 4, "+"));

            // Capturing the exception inside a variable
            var ex = Assert.ThrowsException<CalculationOperationNotSupportedException>(
                () => sut.Calculate(2, 4, "+"));

            Assert.AreEqual("+", ex.Operation);
        }
    }
}
