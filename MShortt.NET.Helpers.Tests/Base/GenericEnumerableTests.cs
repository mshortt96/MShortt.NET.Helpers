using System;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Tests;

public abstract class GenericEnumerableTests
{
    /// <exception cref="ArgumentException"/>
    protected static IEnumerable<T> GetCollectionWithItems<T>(int itemCount, params Func<T>[] itemInitializers)
    {
        if(itemCount < 1)
        {
            throw new ArgumentException("Cannot be less than 1.", nameof(itemCount));
        }

        else
        {
            T[] array = new T[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                array[i] = itemInitializers is null || i >= itemInitializers.Length
                    ? Activator.CreateInstance<T>()
                    : itemInitializers[i].Invoke();
            }

            return array;
        }
    }
}
