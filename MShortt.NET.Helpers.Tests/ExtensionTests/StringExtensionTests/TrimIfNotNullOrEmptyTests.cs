using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.StringExtensionTests;

public class TrimIfNotNullOrEmptyTests
{
    [TestCase(null, ExpectedResult = null)]
    [TestCase("a", ExpectedResult = "a")]
    [TestCase(" a ", ExpectedResult = "a")]
    public string ReturnsCorrectStringWithNoArgumentsTest(string value)
        => value.TrimIfNotNullOrEmpty();

    [TestCase(null, ExpectedResult = null)]
    [TestCase("ab", 'a', ExpectedResult = "b")]
    public string ReturnsCorrectStringWithArgumentsTest(string value, params char[] arguments)
        => value.TrimIfNotNullOrEmpty(arguments);
}
