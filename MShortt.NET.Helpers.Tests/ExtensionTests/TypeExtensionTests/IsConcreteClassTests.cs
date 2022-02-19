using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.TypeExtensionTests;

public class IsConcreteClassTests
{
    [TestCase(typeof(ConcreteClass), ExpectedResult = true)]
    [TestCase(typeof(AbstractClass), ExpectedResult = false)]
    [TestCase(typeof(Struct), ExpectedResult = false)]
    [TestCase(typeof(Interface), ExpectedResult = false)]
    public bool ReturnsCorrectBooleanTest(Type type)
    {
        return type.IsConcreteClass();
    }

    private class ConcreteClass { }
    private abstract class AbstractClass { }
    private struct Struct { }
    private interface Interface { }
}
