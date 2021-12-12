using System;
using System.Collections.Generic;
using System.Linq;

namespace MShortt.NET.Helpers.Tests.ExtensionTests.GenericEnumerableExtensionTests;

public abstract class GenericEnumerableExtensionTestSuite
{
    protected static IEnumerable<int> EmptyCollection { get; }
    protected static IEnumerable<int> SingleItemCollection { get; }

    private static List<IEnumerable<int>> multipleItemCollectionCache;
    private static object multipleItemCollectionWriteLock;

    static GenericEnumerableExtensionTestSuite()
    {
        EmptyCollection = Enumerable.Empty<int>();
        SingleItemCollection = new int[] { 1 };

        multipleItemCollectionCache = new();
        multipleItemCollectionWriteLock = new();
    }

    protected static IEnumerable<int> GetCollectionWithMultipleItems(int itemCount = 2)
    {
        if (itemCount < 2)
            throw new ArgumentException("Cannot be less than 2.", nameof(itemCount));

        //Tests may run in parallel. Thread lock access to cache to prevent race condition.
        lock (multipleItemCollectionWriteLock)
        {
            IEnumerable<int> collection = multipleItemCollectionCache.FirstOrDefault(x => x.Count() == itemCount);
            if (collection is null)
            {
                int[] array = new int[itemCount];
                for (int i = 0; i < itemCount; i++)
                    array[i] = i;

                collection = array;
                multipleItemCollectionCache.Add(collection);
            }

            return collection;
        }
    }
}
