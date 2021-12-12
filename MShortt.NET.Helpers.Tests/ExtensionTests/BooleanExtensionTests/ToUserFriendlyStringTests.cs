using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.BooleanExtensionTests;

public class ToUserFriendlyStringTests
{
    [TestCase(true, ExpectedResult = "Yes")]
    [TestCase(false, ExpectedResult = "No")]
    public string ReturnsCorrectStringTest(bool value)
        => value.ToUserFriendlyString();
}
