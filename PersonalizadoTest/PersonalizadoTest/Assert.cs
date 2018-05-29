namespace PersonalizadoTest
{
    public static class Assert
    {
        public static void IsTrue(bool expression)
        {
            if (!expression)
            {
                throw new TestException("The expression is false");
            }
        }

        public static void IsFalse(bool expression)
        {
            if (expression)
            {
                throw new TestException("The expression is true");
            }
        }

        public static void AreEqual(object expected, object actual)
        {
            if (!expected.Equals(actual))
            {
                throw new TestException("The objects compared are not equal");
            }
        }
    }
}
