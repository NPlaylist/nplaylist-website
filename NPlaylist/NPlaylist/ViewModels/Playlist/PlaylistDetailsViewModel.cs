using NPlaylist.ViewModels.Audio;
using System;
using System.Collections.Generic;

namespace NPlaylist.ViewModels.Playlist
{
    public class PlaylistDetailsViewModel
    {
        public Guid PlaylistId { get; set; }
        public string OwnerUsername { get; set; }
        public DateTime UtcDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<AudioViewModel> Audios { get; set; }
    }
}