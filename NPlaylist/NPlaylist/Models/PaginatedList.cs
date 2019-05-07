using System;
using System.Collections.Generic;

namespace NPlaylist.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }

        public bool HasPreviousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < TotalPages);

        public static PaginatedList<T> Create(
            IEnumerable<T> source,
            int count,
            int pageIndex,
            int pageSize)
        {
            return new PaginatedList<T>(source, count, pageIndex, pageSize);
        }
    }
}
