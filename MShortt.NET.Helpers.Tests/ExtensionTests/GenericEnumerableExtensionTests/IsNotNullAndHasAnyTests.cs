using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class IsNotNullAndHasAnyTests : GenericEnumerableTests
{
    [TestCaseSource(nameof(GetNoPredicateTestCases))]
    public bool NoPredicateReturnsCorrectBooleanTest(IEnumerable<int> collection)
        => collection.IsNotNullAndHasAny();

    [TestCaseSource(nameof(GetPredicateTestCases))]
    public bool WithPredicateReturnsCorrectBooleanTest(IEnumerable<int> collection, Func<int, bool> predicate)
        => collection.IsNotNullAndHasAny(predicate);

    private static IEnumerable<TestCaseData> GetNoPredicateTestCases()
    {
        return new List<TestCaseData>()
        {
            new TestCaseData(null).Returns(false),
            new TestCaseData(Enumerable.Empty<int>()).Returns(false),
            new TestCaseData(GetCollectionWithItems<int>(1)).Returns(true)
        };
    }

    private static IEnumerable<TestCaseData> GetPredicateTestCases()
    {
        List<int> populatedCollection = new() { 1, 2, 3 };

        return new List<TestCaseData>()
        {
            new TestCaseData(null, (int x) => x == 1).Returns(false),
            new TestCaseData(Enumerable.Empty<int>(), (int x) => x == 1).Returns(false),
            new TestCaseData(populatedCollection, (int x) => x == 1).Returns(true),
            new TestCaseData(populatedCollection, (int x) => x == 4).Returns(false)
        };
    }
}
