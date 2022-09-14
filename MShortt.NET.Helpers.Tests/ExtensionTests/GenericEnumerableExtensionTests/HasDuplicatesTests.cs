using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class HasDuplicatesTests : GenericEnumerableTests
{
    [TestCaseSource(nameof(GetUnapplicableTestCases))]
    [TestCaseSource(nameof(GetValueEqualityTestCases))]
    [TestCaseSource(nameof(GetReferenceEqualityTestCases))]
    public void ReturnsCorrectBooleanTest<T>(IEnumerable<T> collection, IEqualityComparer<T> equalityComparer, bool expectedResult)
        => Assert.That(collection.HasDuplicates(equalityComparer) == expectedResult);

    private static IEnumerable<TestCaseData> GetUnapplicableTestCases()
    {
        return new TestCaseData[]
        {
            new TestCaseData(Enumerable.Empty<int>(), null, false),
            new TestCaseData(GetCollectionWithItems<int>(1), null, false)
        };
    }

    private static IEnumerable<TestCaseData> GetValueEqualityTestCases()
    {
        return new TestCaseData[]
        {
            new TestCaseData(new string[] { "hello", "Hello" }, null, false),
            new TestCaseData(new string[] { "hello", "Hello" }, StringComparer.OrdinalIgnoreCase, true)
        };
    }

    private static IEnumerable<TestCaseData> GetReferenceEqualityTestCases()
    {
        object obj = new();

        return new TestCaseData[]
        {
            new TestCaseData(new object[] { obj, new() }, null, false),
            new TestCaseData(new object[] { obj, obj }, null, true)
        };
    }
}
