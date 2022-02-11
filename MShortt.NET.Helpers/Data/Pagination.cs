using System;

namespace MShortt.NET.Helpers.Data
{
    /// <summary>A data class for paging operations.</summary>
    public class Pagination
    {
        private int _page;
        private int _resultsPerPage;

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int Page 
        { 
            get
            {
                return _page;
            }

            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException(nameof(Page));

                _page = value;
            }
        }

        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public int ResultsPerPage
        {
            get 
            { 
                return _resultsPerPage; 
            }

            set
            {
                if(value < 1)
                    throw new ArgumentOutOfRangeException(nameof(ResultsPerPage));

                _resultsPerPage = value;
            }
        }

        public Pagination() : this(1, 1) { }

        public Pagination(int page, int resultsPerPage) 
        {
            Page = page;
            ResultsPerPage = resultsPerPage;
        }

        public static implicit operator Pagination((int, int) tuple)
            => new Pagination(tuple.Item1, tuple.Item2);

        /// <summary>Gets the total number of pages based on the specified item count and maximum results per page.</summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int GetTotalPages(int totalItems, int itemsPerPage)
        {
            if(totalItems < 0)
                throw new ArgumentOutOfRangeException(nameof(totalItems));

            if(itemsPerPage < 1)
                throw new ArgumentOutOfRangeException(nameof(itemsPerPage));

            if (totalItems is 0)
                return 0;

            double resultAsDouble = Math.Ceiling((double)totalItems / itemsPerPage);
            return Convert.ToInt32(resultAsDouble);
        }
    }
}