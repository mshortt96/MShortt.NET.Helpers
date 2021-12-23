using MShortt.NET.Helpers.Converters;
using System;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Tests.ConverterTests.UnixTimeStampConverterTests;

public abstract class UnixTimeStampConverterTests
{
    protected UnixTimeStampConverter Converter { get; }

    public UnixTimeStampConverterTests()
    {
        Converter = new UnixTimeStampConverter();
    }

    protected static IEnumerable<UnixTimeStampConverterTestData> GetTestData()
    {
        return new List<UnixTimeStampConverterTestData>()
        {                
            //Epoch
            new(0, TimeStampKind.Seconds, DateTimeOffset.UnixEpoch),

            //Fractional
            new(0.1, TimeStampKind.Hours, DateTimeOffset.UnixEpoch.AddMinutes(6)),
            new(0.5, TimeStampKind.Hours, DateTimeOffset.UnixEpoch.AddMinutes(30)),
            new(0.1, TimeStampKind.Minutes, DateTimeOffset.UnixEpoch.AddSeconds(6)),
            new(0.5, TimeStampKind.Minutes, DateTimeOffset.UnixEpoch.AddSeconds(30)),
            new(0.1, TimeStampKind.Seconds, DateTimeOffset.UnixEpoch.AddMilliseconds(100)),
            new(0.5, TimeStampKind.Seconds, DateTimeOffset.UnixEpoch.AddMilliseconds(500)),
            new(0.1, TimeStampKind.Milliseconds, DateTimeOffset.UnixEpoch),
            new(0.5, TimeStampKind.Milliseconds, DateTimeOffset.UnixEpoch.AddMilliseconds(1)),

            //Positives
            new(16094593, TimeStampKind.Hours, new DateTimeOffset(3806, 1, 24, 1, 0, 0, TimeSpan.Zero)),
            new(16094593, TimeStampKind.Minutes, new DateTimeOffset(2000, 8, 7, 19, 13, 0, TimeSpan.Zero)),
            new(16094593, TimeStampKind.Seconds, new DateTimeOffset(1970, 7, 6, 6, 43, 13, TimeSpan.Zero)),
            new(16094593, TimeStampKind.Milliseconds, new DateTimeOffset(1970, 1, 1, 4, 28, 14, 593, TimeSpan.Zero)),

            //Negatives
            new(-16094593, TimeStampKind.Hours, new DateTimeOffset(133, 12, 8, 23, 0, 0, TimeSpan.Zero)),
            new(-16094593, TimeStampKind.Minutes, new DateTimeOffset(1939, 5, 27, 4, 47, 0, TimeSpan.Zero)),
            new(-16094593, TimeStampKind.Seconds, new DateTimeOffset(1969, 6, 28, 17, 16, 47, TimeSpan.Zero)),
            new(-16094593, TimeStampKind.Milliseconds, new DateTimeOffset(1969, 12, 31, 19, 31, 45, 407, TimeSpan.Zero)),
        };
    }
}
