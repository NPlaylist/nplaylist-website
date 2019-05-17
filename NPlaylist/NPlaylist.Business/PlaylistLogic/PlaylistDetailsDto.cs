using System;
using System.Collections.Generic;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Business.PlaylistLogic
{
    public class PlaylistDetailsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerUsername { get; set; }
        public DateTime UtcDateTime { get; set; }
        public IEnumerable<Audio> AudioEntries { get; set; }
    }
}
