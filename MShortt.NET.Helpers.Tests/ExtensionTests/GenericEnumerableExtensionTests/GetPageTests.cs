using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class GetPageTests : GenericEnumerableExtensionTestSuite
{
    private static IEnumerable<TestCaseData> TestCases 
    { 
        get 
        {
            IEnumerable<int> collection = GetCollectionWithMultipleItems(3);
            int collectionCount = collection.Count();

            yield return new TestCaseData(EmptyCollection, 1, 1, EmptyCollection);
            yield return new TestCaseData(collection, 1, 0, EmptyCollection);
            yield return new TestCaseData(collection, 1, 1, collection.Take(1));
            yield return new TestCaseData(collection, 1, 2, collection.Take(2));
            yield return new TestCaseData(collection, 2, 2, collection.Skip(2).Take(2));
            yield return new TestCaseData(collection, 1, collectionCount, collection);
            yield return new TestCaseData(collection, 2, collectionCount, EmptyCollection);
        } 
    }

    [Test]
    public void ThrowsExceptionIfPageLessThanOneTest()
        => Assert.Throws<ArgumentException>(() => SingleItemCollection.GetPage(0, 1));

    [TestCaseSource(nameof(TestCases))]
    public void MatchesExpectedSequenceTest(IEnumerable<int> collection, int pageNumber, int resultsPerPage, IEnumerable<int> expectedSequence)
    {
        IEnumerable<int> page = collection.GetPage(pageNumber, resultsPerPage);
        Assert.That(page.SequenceEqual(expectedSequence));
    }
}
