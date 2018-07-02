# PersonalizadoTest Guide

I like to think that there are 4 main parts to creating your own basic test framework which are:
* Identifying
* Asserting
* Messaging/Outputting
* Running

### Part 1 - Identifying The Tests

The first thing you need to do is come up with a way to differentitate your test classes and methods from regular ones. 

Consider how this is done in MSTest:

`[TestClass]` and `[TestMethod]`

This marks the classes and methods with this attribute as test classes and methods, making it easy to identify them for a test run. We can use the same concept for our own test framework by creating some custom attributes:

```using System;

namespace MyCustomTestFramework
{
    public class CustomTestClass : Attribute
    {
    }
}```

```using System;

namespace MyCustomTestFramework
{
    public class CustomTestMethod : Attribute
    {
    }
}```

This should make it easy for us to identify and load the associated test classes and methods when we come to run the tests later on.
