using System;

namespace Produktverwaltung.Pagination
{
    public class PaginationParameter
    {
        private const int maxPageSize = 50;
        private int _pageNumber;
        /// <summary>
        /// Number of page, 0-based
        /// </summary>
        public int PageNumber { 
            get => _pageNumber;
            set => _pageNumber = Math.Max(value, 0); 
        }

        private int _pageSize = 10;
        /// <summary>
        /// Number of elements in page
        /// </summary>
        public int PageSize { 
            get => _pageSize;
            set
            {
                value = Math.Max(value, 0);
                value = Math.Min(value, maxPageSize);
                _pageSize = value;
            }
        }
    }
}
