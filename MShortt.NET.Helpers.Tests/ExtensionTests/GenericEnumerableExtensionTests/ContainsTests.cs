using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class ContainsTests : GenericEnumerableTests
{
    [TestCaseSource(nameof(GetUnapplicableTestCases))]
    [TestCaseSource(nameof(GetSelfReferenceTestCases))]
    [TestCaseSource(nameof(GetValueEqualityTestCases))]
    [TestCaseSource(nameof(GetReferenceEqualityTestCases))]
    public void ReturnsCorrectBooleanTest<T>(IEnumerable<T> collection, IEnumerable<T> secondCollection, IEqualityComparer<T> equalityComparer, bool expectedResult)
        => Assert.That(collection.Contains(secondCollection, equalityComparer) == expectedResult);

    private static IEnumerable<TestCaseData> GetUnapplicableTestCases()
    {
        IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
        IEnumerable<int> populatedCollection = GetCollectionWithItems<int>(1);

        return new TestCaseData[]
        {
            new TestCaseData(emptyCollection, Enumerable.Empty<int>(), null, false),
            new TestCaseData(populatedCollection, emptyCollection, null, false),
            new TestCaseData(emptyCollection, populatedCollection, null, false)
        };
    }

    private static IEnumerable<TestCaseData> GetSelfReferenceTestCases()
    {
        IEnumerable<int> emptyCollection = Enumerable.Empty<int>();
        IEnumerable<int> populatedCollection = GetCollectionWithItems<int>(1);

        return new TestCaseData[]
        {
            new TestCaseData(populatedCollection, populatedCollection, null, true),
            new TestCaseData(emptyCollection, emptyCollection, null, false)
        };
    }

    private static IEnumerable<TestCaseData> GetValueEqualityTestCases()
    {
        IEnumerable<string> collectionUpper = new string[] { "HELLO", "WORLD" };
        IEnumerable<string> collectionLower = new string[] { "hello", "world" };
        IEnumerable<string> collectionConcat = collectionUpper.Concat(collectionLower);

        return new TestCaseData[]
        {
            new TestCaseData(collectionUpper, collectionLower, null, false),
            new TestCaseData(collectionUpper, collectionLower, StringComparer.OrdinalIgnoreCase, true),
            new TestCaseData(collectionUpper, collectionConcat, null, false),
            new TestCaseData(collectionUpper, collectionConcat, StringComparer.OrdinalIgnoreCase, false),
            new TestCaseData(collectionConcat, collectionUpper, null, true),
            new TestCaseData(collectionConcat, collectionUpper, StringComparer.OrdinalIgnoreCase, true)
        };
    }

    private static IEnumerable<TestCaseData> GetReferenceEqualityTestCases()
    {
        object objectOne = new();
        object objectTwo = new();
        IEnumerable<object> collection = new object[] { objectOne, objectTwo };

        return new TestCaseData[]
        {
            new TestCaseData(collection, new object[] { new(), new() }, null, false),
            new TestCaseData(collection, new object[] { objectOne }, null, true),
            new TestCaseData(collection, new object[] { objectOne, new() }, null, false),
            new TestCaseData(collection, new object[] { objectOne, objectTwo }, null, true)
        };
    }
}
