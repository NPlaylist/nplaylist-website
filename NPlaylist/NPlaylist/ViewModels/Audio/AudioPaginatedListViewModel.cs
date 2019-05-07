using System;
using System.Collections.Generic;

namespace NPlaylist.ViewModels.Audio
{
    public class AudioPaginatedListViewModel
    {
        public List<AudioIndexViewModel> Items { get; set; }

        public int TotalNbOfPages { get; set; }

        public int PageIndex { get; set; }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalNbOfPages);
    }
}
