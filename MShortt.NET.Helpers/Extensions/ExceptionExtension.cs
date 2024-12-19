using System;

namespace MShortt.NET.Helpers.Extensions
{
    public static class ExceptionExtension
    {
        ///<summary>Returns the stack trace with leading white space removed. The "at" prefix can also be removed.</summary>
        public static string GetTrimmedStackTrace(this Exception exception, bool removeAt = false)
        {
            string result = exception.StackTrace.TrimStart();
            return removeAt ? result.Substring(3) : result;
        }
    }
}
