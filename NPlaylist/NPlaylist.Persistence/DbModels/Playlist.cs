using System;
using System.Collections.Generic;

namespace NPlaylist.Persistence.DbModels
{
    public class Playlist
    {
        public Guid PlaylistId { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime UtcDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<AudioPlaylists> AudioPlaylists { get; set; }
    }
}
