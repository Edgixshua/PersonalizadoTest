using System;

namespace PersonalizadoTest
{
    public class TestException : Exception
    {
        public TestException(string message)
            : base(message)
        { 
        }
    }
}
