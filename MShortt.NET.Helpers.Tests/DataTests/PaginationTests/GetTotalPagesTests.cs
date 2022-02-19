using MShortt.NET.Helpers.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.DataTests.PaginationTests;

public class GetTotalPagesTests : PagingTests
{
    protected override Action ThrowsIfItemsPerPageInvalidMethodCall
    {
        get
        {
            return () => Pagination.GetTotalPages(1, 0);
        }
    }

    protected override Func<IEnumerable<int>, int, int> ReturnsExpectedIntMethodCall
    {
        get
        {
            return (collection, itemsPerPage) => Pagination.GetTotalPages(collection.Count(), itemsPerPage);
        }
    }
}
