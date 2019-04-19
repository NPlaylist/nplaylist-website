using System;

namespace NPlaylist.Models.Entity
{
    public class AudioMeta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }
        public string Album { get; set; }
    }
}
