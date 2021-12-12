using System;

namespace MShortt.NET.Helpers.Exceptions
{
    /// <summary>An exception that can be thrown when a string is null, empty or just white space.</summary>
    public class NullEmptyWhiteSpaceException : ArgumentException
    {
        public NullEmptyWhiteSpaceException(string paramName = null, Exception innerException = null) : base("String cannot be null, empty or white space.", innerException) { }
    }
}
