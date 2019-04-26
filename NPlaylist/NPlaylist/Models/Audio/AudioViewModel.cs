using System;

namespace NPlaylist.Models.Audio
{
    public class AudioViewModel
    {
        public Guid AudioId { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime UtcCreatedTime { get; set; }
        public string Path { get; set; }
        public AudioMetaViewModel Meta { get; set; }
    }
}