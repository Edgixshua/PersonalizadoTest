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

```public class CustomTestClass : Attribute
{

}
```


```public class CustomTestMethod : Attribute
{

}
```

This should make it easy for us to identify and load the associated test classes and methods when we come to run the tests later on. You can acheive the same result with just a test method attribute but this is less efficient as it will have to search all of your classes due to the lack of a test class atttribute.

### Part 2 - Asserting On Test Outcomes & Outputting The Result

The next thing you are going to need is a way to assert on the outcome of your test methods and then output the result. 

MSTest achieves this through a static class containing all their assertion methods:

https://msdn.microsoft.com/en-us/library/microsoft.visualstudio.testtools.unittesting.assert.aspx

We can again use the same principles for our own test framework by creating a custom assertion static class which will hold all of our assertion methods. For now let's create a basic IsTrue assertion method:

```public static class CustomAssert
{
    public static void IsTrue(bool expression)
    {
        if (!expression)
        {
            
        }
    }
}
```

The question now is how to handle the output of the result. Notice that most test methods written in MSTest are `void` methods that return no result so how can we achieve this?
