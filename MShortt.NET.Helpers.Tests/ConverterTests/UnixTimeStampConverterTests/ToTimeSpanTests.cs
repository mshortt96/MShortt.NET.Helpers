using MShortt.NET.Helpers.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ConverterTests.UnixTimeStampConverterTests;

public class ToTimeSpanTests : UnixTimeStampConverterTests
{
    private static IEnumerable<TestCaseData> TestCases => GetTestData().Select(x =>
    {
        return x.Input.Returns(x.ExpectedOutputAsOffset.Offset);
    });

    [TestCaseSource(nameof(TestCases))]
    public TimeSpan ReturnsExpectedValueTest(double timeStamp, TimeStampKind timeStampKind)
        => Converter.ConvertToTimeSpan(timeStamp, timeStampKind);
}
