using MShortt.NET.Helpers.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Tests.DataTests;

public class ConstructionTests
{
    private static IEnumerable<TestCaseData> OutOfRangeTestCases { get; }

    static ConstructionTests()
    {
        List<TestCaseData> outOfRangeTestCasesList = new();
        for(int i = 0; i >= -1; i--)
        {
            outOfRangeTestCasesList.Add(new(i, true));
            outOfRangeTestCasesList.Add(new(i, false));
        }

        OutOfRangeTestCases = outOfRangeTestCasesList;
    }

    [Test]
    public void ParameterlessConstructorSetsCorrectValuesTest()
    {
        Pagination pagination = new();
        Assert.That(pagination.Page is 1 && pagination.ResultsPerPage is 1);
    }

    [Test]
    public void TupleImplicitCastSetsCorrectValuesTest()
    {
        (int, int) tuple = new(2, 3);
        Pagination pagination = tuple;

        Assert.That(pagination.Page == tuple.Item1 && pagination.ResultsPerPage == tuple.Item2);
    }

    [TestCaseSource(nameof(OutOfRangeTestCases))]
    public void TupleImplicitCastThrowsWhenValueOutOfRangeTest(int value, bool valueIsPageNumber)
    {
        (int, int) tuple = valueIsPageNumber ? new(value, 1) : new(1, value);

        Assert.Throws<ArgumentOutOfRangeException>(() => 
        {
            Pagination pagination = tuple;
        });
    }

    [TestCaseSource(nameof(OutOfRangeTestCases))]
    public void PropertyThrowsWhenValueOutOfRangeTest(int value, bool valueIsPageNumber)
    {
        Pagination pagination = new(1, 1);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            if (valueIsPageNumber)
                pagination.Page = value;

            else
                pagination.ResultsPerPage = value;
        });
    }

    [TestCaseSource(nameof(OutOfRangeTestCases))]
    public void ConstructorThrowsWhenParameterOutOfRangeTest(int value, bool valueIsPageNumber)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => 
        {
            Pagination pagination = valueIsPageNumber ? new Pagination(value, 1) : new Pagination(1, value);
        });
    }
}
