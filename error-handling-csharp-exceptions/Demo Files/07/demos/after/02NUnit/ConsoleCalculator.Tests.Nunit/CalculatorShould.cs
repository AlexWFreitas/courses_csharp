using System;
using NUnit.Framework;

namespace ConsoleCalculator.Tests.Nunit
{
    public class CalculatorShould
    {
        [Test]
        public void ThrowWhenUnsupportedOperation()
        {
            var sut = new Calculator();

            Assert.That(() => sut.Calculate(1, 1, "+"), 
                Throws.TypeOf<CalculationOperationNotSupportedException>());

            Assert.That(() => sut.Calculate(1, 1, "+"), 
                Throws.TypeOf<CalculationOperationNotSupportedException>()
                      .With
                      .Property("Operation").EqualTo("+"));

            //Assert.That(() => sut.Calculate(1, 1, "+"), 
            //    Throws.TypeOf<CalculationException>());

            Assert.That(() => sut.Calculate(1, 1, "+"), 
                Throws.InstanceOf<CalculationException>());


            Assert.Throws<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            var ex = Assert.Throws<CalculationOperationNotSupportedException>(
                () => sut.Calculate(1, 1, "+"));

            Assert.That(ex.Operation, Is.EqualTo("+"));
        }
    }
}
