using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NPlaylist.Models
{
    public class AudioUploadViewModel
    {
        [Required(ErrorMessage = "Select audio file.")]
        [FileExtensions(Extensions = "mp3,wav", ErrorMessage = ("File format is wrong."))]
        public IFormFile File { get; set; }

        public string PublisherId { get; set; }
    }
}