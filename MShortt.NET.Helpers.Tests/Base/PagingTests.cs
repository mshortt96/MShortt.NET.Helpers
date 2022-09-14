using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests;

public abstract class PagingTests : GenericEnumerableTests
{
    protected abstract Action ThrowsIfItemsPerPageInvalidMethodCall { get; }
    protected abstract Func<IEnumerable<int>, int, int> ReturnsExpectedIntMethodCall { get; }
    protected static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            IEnumerable<int> populatedCollection = GetCollectionWithItems<int>(3);

            yield return new TestCaseData(Enumerable.Empty<int>(), 1).Returns(0);
            yield return new TestCaseData(populatedCollection, 1).Returns(3);
            yield return new TestCaseData(populatedCollection, 3).Returns(1);
            yield return new TestCaseData(populatedCollection, 2).Returns(2);
        }
    }

    [Test]
    public void ThrowsIfItemsPerPageInvalidTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            ThrowsIfItemsPerPageInvalidMethodCall.Invoke();
        });
    }

    [TestCaseSource(nameof(TestCases))]
    public int ReturnsExpectedIntTest(IEnumerable<int> collection, int resultsPerPage)
        => ReturnsExpectedIntMethodCall.Invoke(collection, resultsPerPage);
}
