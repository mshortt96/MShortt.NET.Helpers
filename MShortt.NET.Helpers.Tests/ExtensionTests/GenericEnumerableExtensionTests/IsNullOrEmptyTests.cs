using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class IsNullOrEmptyTests : GenericEnumerableExtensionTestSuite
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(null).Returns(true);
            yield return new TestCaseData(EmptyCollection).Returns(true);
            yield return new TestCaseData(SingleItemCollection).Returns(false);
        }
    }

    [TestCaseSource(nameof(TestCases))]
    public bool ReturnsCorrectBooleanTest(IEnumerable<int> collection)
        => collection.IsNullOrEmpty();
}
