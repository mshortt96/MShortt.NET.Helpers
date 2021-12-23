using System;

namespace MShortt.NET.Helpers.Converters
{
    /// <summary>Converts <typeparamref name="TTimeStamp" /> Unix time stamp representations to more informative structures.</summary>
    public abstract class UnixTimeStampConverterBase<TTimeStamp>
    {
        /// <summary>Converts the time stamp to a TimeSpan.</summary>
        public virtual TimeSpan ConvertToTimeSpan(TTimeStamp timeStamp, TimeStampKind timeStampKind)
            => ConvertToDateTimeOffset(timeStamp, timeStampKind).Offset;

        /// <summary>Converts the time stamp to a UTC DateTime.</summary>
        public virtual DateTime ConvertToUtcDateTime(TTimeStamp timeStamp, TimeStampKind timeStampKind)
            => ConvertToDateTimeOffset(timeStamp, timeStampKind).UtcDateTime;

        /// <summary>Converts the time stamp to a DateTimeOffset.</summary>
        public abstract DateTimeOffset ConvertToDateTimeOffset(TTimeStamp timeStamp, TimeStampKind timeStampKind);
    }
}
