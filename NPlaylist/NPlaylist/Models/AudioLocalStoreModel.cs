using Microsoft.AspNetCore.Http;

namespace NPlaylist.Models
{
    public class AudioLocalStoreModel
    {
        public IFormFile FormFile { get; set; }
        public string Path { get; set; }
    }
}
