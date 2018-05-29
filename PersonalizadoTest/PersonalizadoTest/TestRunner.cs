using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PersonalizadoTest
{
    public static class TestRunner
    {
        public static IEnumerable<TestInformation> RunTestSuite(string path)
        {
            var testResults = new List<TestInformation>();

            var assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes();
            var testClasses = GetTestClasses(types);

            foreach (var testClass in testClasses)
            {
                var methods = testClass.GetMethods();
                var testMethods = GetTestMethods(methods);

                if (testMethods.Any())
                {
                    var testClassInstance = Activator.CreateInstance(testClass);

                    foreach (var testMethod in testMethods)
                    {
                        var testInformation = new TestInformation
                        {
                            TestName = $"{testClass.Name}.{testMethod.Name}"
                        };

                        try
                        {
                            testMethod.Invoke(testClassInstance, null);
                            testInformation.Passed = true;
                        }
                        catch (TestException exception)
                        {
                            testInformation.Passed = false;
                            testInformation.Message = exception.Message;
                        }
                        catch (Exception exception)
                        {
                            testInformation.Passed = false;
                            testInformation.Message = $"{exception.GetType()}:{exception.Message}";
                        }

                        testResults.Add(testInformation);
                    }
                }
            }

            return testResults;
        }

        private static IEnumerable<Type> GetTestClasses(IEnumerable<Type> classes)
            => classes.Where(type => type.IsClass && 
               type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(PersonalizadoTestClass)));

        private static IEnumerable<MethodInfo> GetTestMethods(IEnumerable<MethodInfo> methods)
            => methods.Where(method => method.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(PersonalizadoTestMethod)));
    }
}
