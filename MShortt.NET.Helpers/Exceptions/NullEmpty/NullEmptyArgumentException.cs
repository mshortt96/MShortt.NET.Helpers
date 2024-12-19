using System;

namespace MShortt.NET.Helpers.Exceptions
{
    /// <summary>An exception that can be thrown when a string or collection argument is null or empty.</summary>
    public class NullEmptyArgumentException : ArgumentException
    {
        public NullEmptyArgumentException(string paramName = null, Exception innerException = null) : base("Cannot be null or empty.", paramName, innerException) { }
    }
}
