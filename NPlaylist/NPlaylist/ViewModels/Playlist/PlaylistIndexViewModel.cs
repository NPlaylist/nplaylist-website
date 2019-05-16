using System;

namespace NPlaylist.ViewModels.Playlist
{
    public class PlaylistIndexViewModel
    {
        public Guid PlaylistId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public DateTime UtcDateTime { get; set; }
        public int EntriesCount { get; set; }
    }
}