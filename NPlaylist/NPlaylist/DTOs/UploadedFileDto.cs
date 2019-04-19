using Microsoft.AspNetCore.Http;

namespace NPlaylist.DTOs
{
    public class UploadedFileDto
    {
        public IFormFile FormFile { get; set; }
        public string UserId { get; set; }
    }
}
