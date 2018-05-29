using PersonalizadoTest;

namespace CalculatorTests
{
    [PersonalizadoTestClass]
    public class CalculatorTests
    {
        [PersonalizadoTestMethod]
        public void AddingTwoNumbers_OneAndTwo_ProducesThree()
        {
            // Arrange
            const int expectedNumber = 3;
            const int numberOne = 1;
            const int numberTwo = 2;

            // Act
            var actualNumber = Calculator.Methods.Add(numberOne, numberTwo);

            // Assert
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [PersonalizadoTestMethod]
        public void SubtractingOne_FromTwo_ProducesOne()
        {
            // Arrange
            const int expectedNumber = 1;
            const int numberOne = 2;
            const int numberTwo = 1;

            // Act
            var actualNumber = Calculator.Methods.Subtract(numberOne, numberTwo);

            // Assert
            Assert.AreEqual(expectedNumber, actualNumber);
        }
    }
}
