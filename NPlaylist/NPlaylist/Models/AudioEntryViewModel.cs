using System;
using System.ComponentModel.DataAnnotations;

namespace NPlaylist.Models
{
    public class AudioEntryViewModel
    {
        public Guid AudioId { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Created")]
        public DateTime UtcCreatedTime { get; set; }
    }
}
