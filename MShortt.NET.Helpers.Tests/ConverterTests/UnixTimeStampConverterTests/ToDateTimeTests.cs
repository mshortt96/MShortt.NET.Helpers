using MShortt.NET.Helpers.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ConverterTests.UnixTimeStampConverterTests;

public class ToDateTimeTests : UnixTimeStampConverterTests
{
    private static IEnumerable<TestCaseData> TestCases => GetTestData().Select(x =>
    {
        return x.Input.Returns(x.ExpectedOutputAsOffset.UtcDateTime);
    });

    [TestCaseSource(nameof(TestCases))]
    public DateTime ReturnsExpectedValueTest(double timeStamp, TimeStampKind timeStampKind)
        => Converter.ConvertToUtcDateTime(timeStamp, timeStampKind);
}
