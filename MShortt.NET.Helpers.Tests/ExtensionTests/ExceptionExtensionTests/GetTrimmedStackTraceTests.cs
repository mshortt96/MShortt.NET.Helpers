using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.ExceptionExtensionTests;

public class GetTrimmedStackTraceTests
{
    [TestCaseSource(nameof(GetTrimTestCases))]
    public void TrimsTest(Exception exception, bool removeAt)
    {
        try
        {
            throw exception;
        }

        catch
        {
            string trimmedStackTrace = exception.GetTrimmedStackTrace(removeAt);

            bool assertion = removeAt
                ? !Regex.IsMatch(trimmedStackTrace, "^ at .+$")
                : Regex.IsMatch(trimmedStackTrace, "^at .+$");

            Assert.That(assertion);
        }
    }

    private static IEnumerable<TestCaseData> GetTrimTestCases()
    {
        return new TestCaseData[]
        {
            new(new Exception(), true),
            new(new Exception(), false)
        };
    }
}
