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
        TimeSpan expectedTimeSpan = DateTimeOffset.UnixEpoch >= x.ExpectedOutputAsOffset
            ? DateTimeOffset.UnixEpoch - x.ExpectedOutputAsOffset
            : x.ExpectedOutputAsOffset - DateTimeOffset.UnixEpoch;

        return x.Input.Returns(expectedTimeSpan);
    });

    [TestCaseSource(nameof(TestCases))]
    public TimeSpan ReturnsExpectedValueTest(double timeStamp, TimeStampKind timeStampKind)
        => Converter.ConvertToUtcEpochTimeSpan(timeStamp, timeStampKind);
}
