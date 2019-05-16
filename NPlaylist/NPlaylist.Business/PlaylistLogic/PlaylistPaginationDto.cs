using NPlaylist.Persistence.DbModels;
using System.Collections.Generic;

namespace NPlaylist.Business.PlaylistLogic
{
    public class PlaylistPaginationDto
    {
        public List<Playlist> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalNbOfPages { get; set; }
    }
}
