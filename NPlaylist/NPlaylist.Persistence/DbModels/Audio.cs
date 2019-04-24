using System;

namespace NPlaylist.Persistence.DbModels
{
    public class Audio
    {
        public Guid AudioId { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime UtcCreatedTime { get; set; }
        public string Path { get; set; }
        public AudioMeta Meta { get; set; }
    }
}
