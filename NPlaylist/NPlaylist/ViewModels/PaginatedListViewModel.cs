using System.Collections.Generic;

namespace NPlaylist.ViewModels
{
    public class PaginatedListViewModel<T> where  T : class
    {
        public List<T> Items { get; set; }

        public int TotalNbOfPages { get; set; }

        public int PageIndex { get; set; }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalNbOfPages);
    }
}
