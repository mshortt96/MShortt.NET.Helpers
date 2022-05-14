using System;

namespace MShortt.NET.Helpers.Exceptions
{
    /// <summary>An exception that can be thrown when a data query expecting a single result yields more than one.</summary>
    public class MultipleResultsException : Exception
    {
        public MultipleResultsException() : base("The operation yielded more than one result.") { }
    }
}
