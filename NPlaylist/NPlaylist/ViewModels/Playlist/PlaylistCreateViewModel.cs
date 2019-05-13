using System;
using System.ComponentModel.DataAnnotations;

namespace NPlaylist.ViewModels.Playlist
{
    public class PlaylistCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public Guid OwnerId { get; set; }
    }
}