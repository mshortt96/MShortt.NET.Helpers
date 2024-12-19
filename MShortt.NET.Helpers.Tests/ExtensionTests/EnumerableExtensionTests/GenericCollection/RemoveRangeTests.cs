using System;
using System.Collections.Generic;
using System.Linq;
using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.EnumerableExtensionTests.GenericCollection;

public class RemoveRangeTests : EnumerableTests
{
    [TestCaseSource(nameof(GetSequenceEqualityTestCases))]
    public void ExpectedSequenceMetTest<T>(ICollection<T> collection, IEnumerable<T> itemsToRemove, IEnumerable<T> expectedResult)
    {
        collection.RemoveRange(itemsToRemove);
        Assert.That(collection.SequenceEqual(expectedResult));
    }

    [Test]
    public void ReadOnlyCollectionNotSupportedTest()
    {
        int[] readOnlyCollection = new[] { 1, 2 };
        Assert.Throws<NotSupportedException>(() => 
        {
            readOnlyCollection.RemoveRange(new[] { 1 });
        });
    }

    private static IEnumerable<TestCaseData> GetSequenceEqualityTestCases()
    {
        List<TestCaseData> cases = new List<TestCaseData>
        {
            new TestCaseData(new List<int> { 1, 2 }, new int[] { 1 }, new int[] { 2 }),
            new TestCaseData(new List<int> { 1, 2 }, new int[] { 1, 2 }, Enumerable.Empty<int>()),
            new TestCaseData(new List<int> { 1, 2 }, new int[] { 3, 4 }, new int[] { 1, 2 }),
            new TestCaseData(new List<int> { 1, 2 }, Enumerable.Empty<int>(), new int[] { 1, 2 })
        };

        List<int> selfReferenceCaseData = new List<int> { 1, 2 };
        cases.Add(new(selfReferenceCaseData, selfReferenceCaseData, Enumerable.Empty<int>()));

        return cases;
    }
}
