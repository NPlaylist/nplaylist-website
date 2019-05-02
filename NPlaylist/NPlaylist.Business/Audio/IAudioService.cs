using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Business.Audio
{
    public interface IAudioService
    {
        Task UploadAsync(AudioUploadDto uploadViewModel, CancellationToken ct);
    }
}