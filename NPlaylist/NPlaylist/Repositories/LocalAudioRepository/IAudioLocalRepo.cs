using NPlaylist.Models;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Repositories.LocalAudioRepository
{
    public interface IAudioLocalRepo
    {
        Task StoreAsync(AudioLocalStoreModel audioLocalStoreModel, CancellationToken ct);
    }
}
