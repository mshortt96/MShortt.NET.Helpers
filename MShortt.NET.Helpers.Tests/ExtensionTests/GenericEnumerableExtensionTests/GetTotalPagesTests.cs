using MShortt.NET.Helpers.Extensions;
using System;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public class GetTotalPagesTests : PagingTests
{
    protected override Action ThrowsIfItemsPerPageInvalidMethodCall 
    { 
        get
        { 
            return () => GetCollectionWithItems<int>(1).GetTotalPages(0);
        } 
    }

    protected override Func<IEnumerable<int>, int, int> ReturnsExpectedIntMethodCall
    {
        get
        {
            return (collection, resultsPerPage) => collection.GetTotalPages(resultsPerPage);
        }
    }
}
