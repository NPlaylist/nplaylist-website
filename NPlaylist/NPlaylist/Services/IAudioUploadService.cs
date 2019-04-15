
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NPlaylist.Services
{
    public interface IAudioUploadService
    {
        Task Upload(IFormFile file);
    }
}
