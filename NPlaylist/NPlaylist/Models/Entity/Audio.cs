using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPlaylist.Models.Entity
{
    public class Audio
    {
        public Guid AudioId { get; set; } = new Guid();
        public DateTime Created { get; } = DateTime.Now;
        public string Path { get; set; }
        public Guid UserId { get; set; }
        public AudioMeta Meta { get; set; }
    }
}
