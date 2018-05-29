using System;
using PersonalizadoTest;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var testResults = TestRunner.RunTestSuite(@"C:\Users\joshuas\source\repos\PersonalizadoTest\PersonalizadoTest\CalculatorTests\bin\Debug\netstandard2.0\CalculatorTests.dll");

            foreach (var result in testResults)
            {
                Console.WriteLine($"{result.TestName}: {result.Passed} ({result.Message})");
            }
        }
    }
}
