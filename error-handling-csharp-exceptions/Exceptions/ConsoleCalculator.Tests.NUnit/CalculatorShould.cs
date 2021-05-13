using NUnit.Framework;

namespace ConsoleCalculator.Tests.NUnit
{
    public class CalculatorShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Calculator();

            Assert.That
                (
                    () => sut.Calculate(1, 1, "+"),
                    Throws.TypeOf<CalculationOperationNotSupportedException>()
                );

            Assert.That
                (
                    () => sut.Calculate(1, 1, "+"),
                    Throws.TypeOf<CalculationOperationNotSupportedException>()
                    .With
                    .Property("Operation").EqualTo("+")
                );

            /* Throws.TypeOf - Expects the exact exception.
             * Therefore test fails.
             * 
            Assert.That
                (
                    () => sut.Calculate(1, 1, "+"),
                    Throws.TypeOf<CalculationException>()
                );

            */

            Assert.That
                (
                    () => sut.Calculate(1, 1, "+"),
                    Throws.InstanceOf<CalculationException>()
                );

            Assert.Throws<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            var ex = Assert.Throws<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            Assert.AreEqual("+", ex.Operation);
        }
    }
}