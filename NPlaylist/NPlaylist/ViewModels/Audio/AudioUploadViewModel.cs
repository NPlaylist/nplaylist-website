using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NPlaylist.Attributes;

namespace NPlaylist.ViewModels.Audio
{
    public class AudioUploadViewModel
    {
        [Required(ErrorMessage = "Select audio file.")]
        [AllowExtensions(Extensions = "mp3,wav", ErrorMessage = ("File format is wrong."))]
        public IFormFile File { get; set; }

        public Guid PublisherId { get; set; }
    }
}