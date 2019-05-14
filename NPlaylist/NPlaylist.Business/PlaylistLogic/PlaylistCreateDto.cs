using System;

namespace NPlaylist.Business.PlaylistLogic
{
    public class PlaylistCreateDto
    {
        public string Description { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; }
    }
}