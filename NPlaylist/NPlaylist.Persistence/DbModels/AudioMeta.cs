using System;

namespace NPlaylist.Persistence.DbModels
{
    public class AudioMeta
    {
        public Guid AudioMetaId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Album { get; set; }
    }
}
