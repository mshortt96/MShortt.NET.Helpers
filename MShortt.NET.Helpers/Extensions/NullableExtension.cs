namespace MShortt.NET.Helpers.Extensions
{
    public static class NullableExtension
    {
        /// <summary>Evaluates if the nullable is not null and has the specified value. <b>The nullable will be boxed by this call</b>.</summary>
        public static bool IsNotNullAndHasValue<T>(this T? nullable, T expectedValue) where T : struct
            => nullable.HasValue && nullable.Value.Equals(expectedValue);
    }
}
