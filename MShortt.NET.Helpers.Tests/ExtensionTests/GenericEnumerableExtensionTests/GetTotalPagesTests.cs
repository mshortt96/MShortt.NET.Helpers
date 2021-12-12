using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class GetTotalPagesTests : GenericEnumerableExtensionTestSuite
{

    private static IEnumerable<TestCaseData> TestCases 
    { 
        get 
        {
            yield return new TestCaseData(EmptyCollection, 1).Returns(0);
            yield return new TestCaseData(GetCollectionWithMultipleItems(3), 0).Returns(0);
            yield return new TestCaseData(GetCollectionWithMultipleItems(3), 1).Returns(3);
            yield return new TestCaseData(GetCollectionWithMultipleItems(3), 3).Returns(1);
            yield return new TestCaseData(GetCollectionWithMultipleItems(3), 2).Returns(2);
        } 
    }

    [TestCaseSource(nameof(TestCases))]
    public long ReturnsExpectedLongTest(IEnumerable<int> collection, int resultsPerPage)
        => collection.GetTotalPages(resultsPerPage);
}
