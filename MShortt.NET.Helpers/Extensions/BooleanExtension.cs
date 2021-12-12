namespace MShortt.NET.Helpers.Extensions
{
    public static class BooleanExtension
    {
        /// <summary>Returns a "Yes" or "No" string depending on whether the boolean is true or false, respectively.</summary>
        public static string ToUserFriendlyString(this bool value)
            => value ? "Yes" : "No";
    }
}
