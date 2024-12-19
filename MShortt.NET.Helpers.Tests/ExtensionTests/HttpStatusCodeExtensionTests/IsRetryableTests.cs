using MShortt.NET.Helpers.Extensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.HttpStatusCodeExtensionTests;

public class IsRetryableTests
{
    [TestCaseSource(nameof(GetTestCases))]
    public void ReturnsCorrectBooleanTest(HttpStatusCode statusCode, bool expectedResult)
    {
        Assert.That(statusCode.IsRetryable() == expectedResult);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        List<TestCaseData> testCases = new();

        HttpStatusCode[] trueCases = new[]
        {
            HttpStatusCode.GatewayTimeout,
            HttpStatusCode.BadGateway,
            HttpStatusCode.RequestTimeout,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.TooManyRequests
        };

        HttpStatusCode[] falseCases = Enum.GetValues<HttpStatusCode>()
            .Where(x => !trueCases.Contains(x))
            .ToArray();

        foreach(HttpStatusCode statusCode in trueCases)
        {
            testCases.Add(new(statusCode, true));
        }

        foreach(HttpStatusCode statusCode in falseCases)
        {
            testCases.Add(new(statusCode, false));
        }

        return testCases;
    }
}
