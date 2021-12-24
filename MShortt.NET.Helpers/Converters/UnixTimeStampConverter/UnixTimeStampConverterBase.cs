using System;

namespace MShortt.NET.Helpers.Converters
{
    /// <summary>Converts <typeparamref name="TTimeStamp" /> Unix time stamp representations to more informative structures.</summary>
    public abstract class UnixTimeStampConverterBase<TTimeStamp>
    {
        internal static DateTimeOffset UnixEpoch { get; }

        static UnixTimeStampConverterBase()
        {
            UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        }

        /// <summary>Calculates how long before, or after, the time stamp is to the Unix epoch in UTC, and returns the result in a TimeSpan.</summary>
        public virtual TimeSpan ConvertToUtcEpochTimeSpan(TTimeStamp timeStamp, TimeStampKind timeStampKind)
        {
            DateTimeOffset offset = ConvertToDateTimeOffset(timeStamp, timeStampKind);

            return offset >= UnixEpoch 
                ? offset - UnixEpoch 
                : UnixEpoch - offset;
        }

        /// <summary>Converts the time stamp to a UTC DateTime.</summary>
        public virtual DateTime ConvertToUtcDateTime(TTimeStamp timeStamp, TimeStampKind timeStampKind)
            => ConvertToDateTimeOffset(timeStamp, timeStampKind).UtcDateTime;

        /// <summary>Converts the time stamp to a DateTimeOffset.</summary>
        public abstract DateTimeOffset ConvertToDateTimeOffset(TTimeStamp timeStamp, TimeStampKind timeStampKind);
    }
}
