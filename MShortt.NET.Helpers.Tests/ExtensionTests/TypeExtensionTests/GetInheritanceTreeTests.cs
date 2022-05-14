using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.TypeExtensionTests;

public class GetInheritanceTreeTests
{
    [TestCaseSource(nameof(GetTestCases))]
    public void CreatedExpectedSequenceTest(Type type, bool excludeInterfaces, Func<Type, bool> predicate, IEnumerable<Type> expectedResult)
    {
        IEnumerable<Type> result = type.GetInheritanceTree(excludeInterfaces, predicate);
        Assert.That(result.SequenceEqual(expectedResult));
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        return new TestCaseData[]
        {
            new(typeof(MockClass), false, null, new Type[] { typeof(ParentMockInterface), typeof(object) }),
            new(typeof(MockClass), true, null, new Type[] { typeof(object) }),
            new(typeof(MockClass), false, new Func<Type, bool>(x => x != typeof(object)), new Type[] { typeof(ParentMockInterface) }),
            new(typeof(ChildMockInterface), false, null, new Type[] { typeof(ParentMockInterface) }),
            new(typeof(ChildMockInterface), false, new Func<Type, bool>(x => x != typeof(ParentMockInterface)), Enumerable.Empty<Type>()),
            new(typeof(ChildMockInterface), true, null, Enumerable.Empty<Type>()),
            new(typeof(ParentMockInterface), false, null, Enumerable.Empty<Type>())
        };
    }

    public class MockClass : ParentMockInterface { }
    public interface ChildMockInterface : ParentMockInterface { }
    public interface ParentMockInterface { }
}
