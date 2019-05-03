using System;
using System.ComponentModel.DataAnnotations;

namespace NPlaylist.Models.Audio
{
    public class AudioMetaViewModel
    {
        public Guid AudioMetaId { get; set; }

        [StringLength(64)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [StringLength(64)]
        public string Author { get; set; }

        [StringLength(64)]
        public string Album { get; set; }
    }
}
