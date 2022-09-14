using MShortt.NET.Helpers.Data;
using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class GetPageTests : GenericEnumerableTests
{
    private static IEnumerable<TestCaseData> TestCases 
    {
        get
        {
            IEnumerable<int> emptyCollection = Enumerable.Empty<int>();

            IEnumerable<int> populatedCollection = GetCollectionWithItems<int>(3);
            int populatedCollectionCount = populatedCollection.Count();

            return new List<TestCaseData>()
            {
                new(emptyCollection, 1, 1, emptyCollection),
                new(populatedCollection, 1, 1, populatedCollection.Take(1)),
                new(populatedCollection, 1, 2, populatedCollection.Take(2)),
                new(populatedCollection, 2, 2, populatedCollection.Skip(2).Take(2)),
                new(populatedCollection, 1, populatedCollectionCount, populatedCollection),
                new(populatedCollection, 2, populatedCollectionCount, emptyCollection)
            };
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public void ThrowsExceptionIfParameterLessThanOneTest(bool pageNumber)
    {
        IEnumerable<int> collection = GetCollectionWithItems<int>(1);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            IEnumerable<int> result = pageNumber ? collection.GetPage(0, 1) : collection.GetPage(1, 0);
        });
    }

    [TestCaseSource(nameof(TestCases))]
    public void MatchesExpectedSequenceTest(IEnumerable<int> collection, int pageNumber, int resultsPerPage, IEnumerable<int> expectedSequence)
    {
        IEnumerable<int> page = collection.GetPage(pageNumber, resultsPerPage);
        AssertSequencesEqual(page, expectedSequence);
    }

    [TestCaseSource(nameof(TestCases))]
    public void OverloadMatchesExpectedSequenceTest(IEnumerable<int> collection, int pageNumber, int resultsPerPage, IEnumerable<int> expectedSequence)
    {
        Pagination pagination = new(pageNumber, resultsPerPage);
        IEnumerable<int> page = collection.GetPage(pagination);
        AssertSequencesEqual(page, expectedSequence);
    }

    private void AssertSequencesEqual(IEnumerable<int> sequenceOne, IEnumerable<int> sequenceTwo)
        => Assert.That(sequenceOne.SequenceEqual(sequenceTwo));
}
