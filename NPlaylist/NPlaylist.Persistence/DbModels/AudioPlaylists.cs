using System;

namespace NPlaylist.Persistence.DbModels
{
    public class AudioPlaylists
    {
        public Guid AudioId { get; set; }
        public Audio Audio { get; set; }
        
        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
    }
}
