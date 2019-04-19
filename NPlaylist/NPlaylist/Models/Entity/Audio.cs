using System;

namespace NPlaylist.Models.Entity
{
    public class Audio
    {
        public Guid AudioId { get; set; }
        public DateTime Created { get; }
        public string Path { get; set; }
        public Guid UserId { get; set; }
        public AudioMeta Meta { get; set; }

        public Audio()
        {
            AudioId = Guid.NewGuid();
            Created = DateTime.Now;
        }
    }
}
