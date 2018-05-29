using System;
using PersonalizadoTest;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var testResults = TestRunner.RunTestSuite("");

            foreach (var result in testResults)
            {
                Console.WriteLine($"{result.TestName}: {result.Passed} ({result.Message})");
            }
        }
    }
}
