using System;

namespace MShortt.NET.Helpers.Exceptions
{
    /// <summary>An exception that can be thrown when a string argument is null, empty or just white space.</summary>
    public class NullEmptyWhiteSpaceArgumentException : ArgumentException
    {
        public NullEmptyWhiteSpaceArgumentException(string paramName = null, Exception innerException = null) 
            : base("String cannot be null, empty or white space.", paramName, innerException) { }
    }
}
