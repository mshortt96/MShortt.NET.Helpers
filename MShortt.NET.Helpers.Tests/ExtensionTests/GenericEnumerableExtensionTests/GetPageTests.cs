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
            IEnumerable<int> collection = GetCollectionWithMultipleItems(3);
            int collectionCount = collection.Count();

            return new List<TestCaseData>()
            {
                new(EmptyCollection, 1, 1, EmptyCollection),
                new(collection, 1, 1, collection.Take(1)),
                new(collection, 1, 2, collection.Take(2)),
                new(collection, 2, 2, collection.Skip(2).Take(2)),
                new(collection, 1, collectionCount, collection),
                new(collection, 2, collectionCount, EmptyCollection)
            };
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public void ThrowsExceptionIfParameterLessThanOneTest(bool pageNumber)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            IEnumerable<int> result = pageNumber ? SingleItemCollection.GetPage(0, 1) : SingleItemCollection.GetPage(1, 0);
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
