# PersonalizadoTest Guide

I like to think that there are 3 main parts to creating your own basic test framework which are:
* Identifying
* Asserting & Outputting
* Running

### Part 1 - Identifying The Tests

The first thing you need to do is come up with a way to differentitate your test classes and methods from regular ones. 

Consider how this is done in MSTest:

`[TestClass]` and `[TestMethod]`

This marks the classes and methods with this attribute as test classes and methods, making it easy to identify them for a test run. We can use the same concept for our own test framework by creating some custom attributes:

```
public class CustomTestClass : Attribute
{

}
```


```
public class CustomTestMethod : Attribute
{

}
```

This should make it easy for us to identify and load the associated test classes and methods when we come to run the tests later on. You can acheive the same result with just a test method attribute but this is less efficient as it will have to search all of your classes due to the lack of a test class atttribute.

### Part 2 - Asserting On Test Outcomes & Outputting The Result

The next thing you are going to need is a way to assert on the outcome of your test methods and then output the result. 

MSTest achieves this through a static class containing all their assertion methods:

https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.testtools.unittesting.assert.aspx

We can again use the same principles for our own test framework by creating a custom assertion static class which will hold all of our assertion methods. For now let's create a basic IsTrue assertion method:

```
public static class CustomAssert
{
    public static void IsTrue(bool expression)
    {
        if (!expression)
        {
            // Handle result
        }
    }
}
```

The question now is how to handle the output of the result. Notice that most test methods written in MSTest are `void` methods that return no result so how can we achieve this?

### Part 3 - Running The Tests

The approach I decided to take was to create a `TestInformation` class to store all the relevant details during the test run:

```
public class TestInformation
    {
        public string TestName { get; set; }

        public bool Passed { get; set; }

        public string Message { get; set; }
    }
```

This allows us to store the name of the test, whether it passed and the message it output. We can then add these to a list which is returned at the end of the test run. I then also created a custom test exception to throw when one of our custom tests fails:

```
public class TestException : Exception
    {
        public TestException(string message)
            : base(message)
        { 
        }
    }
```

In terms of running the tests, we need the test project assembly information first before proceeding which is where the Reflection library comes in:

https://msdn.microsoft.com/en-us/library/system.reflection(v=vs.110).aspx

Using this library to interact with the test assembly, we can first extract out all the classes:

```
var assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes();
            var testClasses = GetTestClasses(types);
```

Then pull out the test classes and methods by searching for our custom test attribute:

```
private static IEnumerable<Type> GetTestClasses(IEnumerable<Type> classes)
    => classes.Where(type => type.IsClass && 
        type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(PersonalizadoTestClass)));

private static IEnumerable<MethodInfo> GetTestMethods(IEnumerable<MethodInfo> methods)
    => methods.Where(method => method.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(PersonalizadoTestMethod)));
```

Finally, to actually the run the test methods I created a new instance of each of the test classes and then ran each test method contained within, making sure to populate `TestInformation` objects as the run proceeds:

```
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
```