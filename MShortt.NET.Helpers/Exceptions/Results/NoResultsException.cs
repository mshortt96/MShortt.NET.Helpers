using System;

namespace MShortt.NET.Helpers.Exceptions
{
    /// <summary>An exception that can be thrown when an operation yields no results.</summary>
    public class NoResultsException : Exception
    {
        public NoResultsException() : base("The operation did not yield any results.") { }
    }
}
