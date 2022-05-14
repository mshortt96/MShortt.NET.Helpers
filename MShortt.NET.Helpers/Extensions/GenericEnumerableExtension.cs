using MShortt.NET.Helpers.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MShortt.NET.Helpers.Extensions
{
    public static class GenericEnumerableExtension
    {
        /// <summary>
        ///     Returns a new collection with all instances of the given item removed. If no Equality Comparer is provided, equality checking will
        ///     default to being either value-based or reference-based depending on the type.
        /// </summary>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, T itemToExclude, IEqualityComparer<T> equalityComparer = null)
        {
            return equalityComparer is null
                ? collection.Where(x => !x.Equals(itemToExclude))
                : collection.Where(x => !equalityComparer.Equals(x, itemToExclude));
        }


        /// <summary>Indicates whether the specified collection is null or contains no elements.</summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
            => collection == null || !collection.Any();

        /// <summary>
        ///     Indicates whether the specified collection is not null and contains at least one element. For the latter
        ///     evaluation, a predicate may be provided to instead check for at least one element that matches a condition.
        /// </summary>
        public static bool IsNotNullAndHasAny<T>(this IEnumerable<T> collection, Func<T, bool> predicate = null)
        {
            if (collection == null)
            {
                return false;
            }

            else
            {
                return predicate == null ? collection.Any() : collection.Any(predicate);
            }
        }

        /// <summary>Gets the zero-based index number of the last element in the collection, or -1 if the collection is empty.</summary>
        public static int GetFinalIndexNumber<T>(this IEnumerable<T> collection)
            => collection.Any() ? collection.Count() - 1 : -1;

        /// <summary>Creates a new ObservableCollection reference from the collection.</summary>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
            => new ObservableCollection<T>(collection);

        /// <summary>Gets the total number of pages for paginating this collection, based on the specified maximum results per page.</summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetTotalPages<T>(this IEnumerable<T> collection, int resultsPerPage)
            => Pagination.GetTotalPages(collection.Count(), resultsPerPage);

        /// <summary>Gets the elements in the collection pertaining to the specified Pagination data.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> collection, Pagination pagination)
            => GetPage(collection, pagination.Page, pagination.ResultsPerPage);

        /// <summary>Gets the elements in the collection pertaining to the specified page number and maximum results per page.</summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> collection, int page, int resultsPerPage)
        {
            if (page < 1)
                throw new ArgumentOutOfRangeException(nameof(page));

            if (resultsPerPage < 1)
                throw new ArgumentOutOfRangeException(nameof(resultsPerPage));

            if (!collection.Any())
                return collection;

            int collectionOffset = resultsPerPage * (page - 1);
            return collection.Skip(collectionOffset).Take(resultsPerPage);
        }
    }
}
