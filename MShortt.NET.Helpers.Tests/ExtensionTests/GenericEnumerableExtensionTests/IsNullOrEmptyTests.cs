using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class IsNullOrEmptyTests : GenericEnumerableTests
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(null).Returns(true);
            yield return new TestCaseData(Enumerable.Empty<int>()).Returns(true);
            yield return new TestCaseData(GetCollectionWithItems<int>(1)).Returns(false);
        }
    }

    [TestCaseSource(nameof(TestCases))]
    public bool ReturnsCorrectBooleanTest(IEnumerable<int> collection)
        => collection.IsNullOrEmpty();
}
