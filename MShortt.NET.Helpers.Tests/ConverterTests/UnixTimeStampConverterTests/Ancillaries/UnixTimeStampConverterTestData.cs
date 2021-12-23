using MShortt.NET.Helpers.Converters;
using NUnit.Framework;
using System;

namespace MShortt.NET.Helpers.Tests.ConverterTests.UnixTimeStampConverterTests
{
    public record UnixTimeStampConverterTestData
    {
        public TestCaseData Input { get; set; }
        public DateTimeOffset ExpectedOutputAsOffset { get; set; }

        public UnixTimeStampConverterTestData() { }

        public UnixTimeStampConverterTestData(double timeStamp, TimeStampKind timeStampKind, DateTimeOffset expectedOutputAsOffset)
        {
            Input = new(timeStamp, timeStampKind);
            ExpectedOutputAsOffset = expectedOutputAsOffset;
        }
    }
}
