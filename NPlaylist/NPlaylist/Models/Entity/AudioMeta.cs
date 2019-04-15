using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPlaylist.Models.Entity
{
    public class AudioMeta
    {
        public Guid Id { get; set; } = new Guid();
        public string Title { get; set; }
        public string Author { get; set; }
        public string Album { get; set; }
    }
}
