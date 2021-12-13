using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.StringExtensionTests;

public class RemoveRedundantSpacingTests
{
    [TestCase(" ", ExpectedResult = "")]
    [TestCase("   ", ExpectedResult = "")]
    [TestCase(" a ", ExpectedResult = "a")]
    [TestCase("   a   ", ExpectedResult = "a")]
    [TestCase(" a b ", ExpectedResult = "a b")]
    [TestCase("   a   b   ", ExpectedResult = "a b")]
    public string ReturnsCorrectStringTest(string value)
        => value.RemoveRedundantSpacing();
}
