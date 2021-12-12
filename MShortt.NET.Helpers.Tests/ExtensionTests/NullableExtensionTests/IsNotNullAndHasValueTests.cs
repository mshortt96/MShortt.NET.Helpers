using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.NullableExtensionTests;

public class IsNotNullAndHasValueTests
{
    [TestCase(0, 0, ExpectedResult = true)]
    [TestCase(0, 1, ExpectedResult = false)]
    public bool ReturnsCorrectBooleanWhenNotNullTest(int valueToWrap, int valueToCompare)
    {
        int? nullableWrapper = valueToWrap;
        return nullableWrapper.IsNotNullAndHasValue(valueToCompare);
    }

    [Test]
    public void ReturnsFalseWhenNullTest()
    {
        int? nullable = null;
        Assert.That(!nullable.IsNotNullAndHasValue(0));
    }
}
