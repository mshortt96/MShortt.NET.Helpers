using System;
using System.Collections.Generic;

namespace MShortt.NET.Helpers.Extensions
{
    public static class GenericCollectionExtension
    {
        /// <summary>Removes the specified elements from the collection.</summary>
        /// <exception cref="NotSupportedException"></exception>
        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if(collection == items)
            {
                collection.Clear();
            }

            else
            {
                foreach (T item in items)
                {
                    collection.Remove(item);
                }
            }
        }
    }
}
