using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class ExceptTests : GenericEnumerableTests
{
    [TestCaseSource(nameof(GetStringTestCases))]
    [TestCaseSource(nameof(GetReferenceEqualityTestCases))]
    public void CreatesExpectedSequenceTest<T>(IEnumerable<T> collection, T itemToExclude, IEnumerable<T> expectedResult, IEqualityComparer<T> equalityComparer = null)
    {
        Assert.That(collection.Except(itemToExclude, equalityComparer).SequenceEqual(expectedResult));
    }

    private static IEnumerable<TestCaseData> GetStringTestCases()
    {
        string exclusion = "Goodbye World";

        string[] collection = new[]
        {
            "Hello World",
            "Goodbye World",
            "goodbye world"
        };

        return new TestCaseData[]
        {
            new TestCaseData(collection, exclusion, new string[] { "Hello World", "goodbye world" }, null),
            new TestCaseData(collection, exclusion, new string[] { "Hello World" }, StringComparer.OrdinalIgnoreCase)
        };
    }

    private static IEnumerable<TestCaseData> GetReferenceEqualityTestCases()
    {
        object referenceOne = new();
        object referenceTwo = new();

        object[] collection = new[]
        {
            referenceOne,
            referenceOne,
            referenceTwo
        };

        return new TestCaseData[]
        {
            new TestCaseData(collection, referenceOne, new object[] { referenceTwo }, null),
        };
    }
}