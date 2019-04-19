using System.Threading;
using System.Threading.Tasks;
using NPlaylist.DTOs;

namespace NPlaylist.Services.AudioService
{
    public interface IAudioService
    {
        Task Upload(UploadedFileDto uploadedFile, CancellationToken ct);
    }
}
