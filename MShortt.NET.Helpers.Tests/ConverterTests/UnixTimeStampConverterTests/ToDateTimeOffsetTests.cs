using MShortt.NET.Helpers.Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ConverterTests.UnixTimeStampConverterTests;

public class ToDateTimeOffsetTests : UnixTimeStampConverterTests
{
    private static IEnumerable<TestCaseData> TestCases => GetTestData().Select(x =>
    {
        return x.Input.Returns(x.ExpectedOutputAsOffset);
    });

    [TestCaseSource(nameof(TestCases))]
    public DateTimeOffset ReturnsExpectedValueTest(double timeStamp, TimeStampKind timeStampKind)
        => Converter.ConvertToDateTimeOffset(timeStamp, timeStampKind);
}
