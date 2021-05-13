using Xunit;

namespace ConsoleCalculator.Tests.XUnit
{
    public class CalculatorShould
    {
        [Fact]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Calculator();

            Assert.Throws<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+") );

            // Fails because it expects the exact type.
            /*
            Assert.Throws<CalculationException>(
                () => sut.Calculate(1, 1, "+")); 
            */

            Assert.ThrowsAny<CalculationException>(
                () => sut.Calculate(1, 1, "+"));

            var ex = Assert.Throws<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            Assert.Equal("+", ex.Operation);
        }
    }
}
