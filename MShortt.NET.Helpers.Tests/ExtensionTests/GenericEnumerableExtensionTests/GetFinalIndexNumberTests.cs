using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class GetFinalIndexNumberTests : GenericEnumerableTests
{
    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(Enumerable.Empty<int>()).Returns(-1);
            yield return new TestCaseData(GetCollectionWithItems<int>(1)).Returns(0);
            yield return new TestCaseData(GetCollectionWithItems<int>(2)).Returns(1);
        }
    }

    [TestCaseSource(nameof(TestCases))]
    public int ReturnsExpectedIntTest(IEnumerable<int> collection)
        => collection.GetFinalIndexNumber();

    [Test]
    public void ThrowsExceptionIfCollectionNullTest()
    {
        IEnumerable<int> collection = null;
        Assert.Throws<ArgumentNullException>(() => collection.GetFinalIndexNumber());
    }
}
