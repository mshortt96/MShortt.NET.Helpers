using System;

namespace MShortt.NET.Helpers.Converters
{
    /// <inheritdoc />
    public class UnixTimeStampConverter : UnixTimeStampConverterBase<double>
    {
        public override DateTimeOffset ConvertToDateTimeOffset(double timeStamp, TimeStampKind timeStampKind)
        {
            DateTimeOffset offset = UnixEpoch;

            switch (timeStampKind)
            {
                case TimeStampKind.Hours:
                    offset = offset.AddHours(timeStamp);
                    break;

                case TimeStampKind.Minutes:
                    offset = offset.AddMinutes(timeStamp);
                    break;

                case TimeStampKind.Seconds:
                    offset = offset.AddSeconds(timeStamp);
                    break;

                case TimeStampKind.Milliseconds:
                    offset = offset.AddMilliseconds(timeStamp);
                    break;

                default:
                    throw new NotImplementedException($@"The {nameof(TimeStampKind)} value ""{timeStamp}"" is not yet supported by this method.");
            }

            return offset;
        }
    }
}
