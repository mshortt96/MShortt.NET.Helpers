using System;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Tests;

public abstract class EnumerableTests
{
    /// <exception cref="ArgumentException"/>
    protected static IList<T> GetListWithItems<T>(int itemCount, params Func<T>[] itemInitializers)
    {
        if(itemCount < 1)
        {
            throw new ArgumentException("Cannot be less than 1.", nameof(itemCount));
        }

        else
        {
            IList<T> list = new List<T>();
            for (int i = 0; i < itemCount; i++)
            {
                T item = itemInitializers is null || i >= itemInitializers.Length
                    ? Activator.CreateInstance<T>()
                    : itemInitializers[i].Invoke();

                list.Add(item);
            }

            return list;
        }
    }
}
