using System;
using System.ComponentModel.DataAnnotations;

namespace NPlaylist.Models
{
    public class AudioEntryViewModel
    {
        public Guid Id { get; set; }
        [Display(Name="Full Name")]
        public string FullName { get; set; }
        public string Extension { get; set; }
        public string Publisher { get; set; }
        public DateTime UtcCreatedTime { get; set; }
    }
}
