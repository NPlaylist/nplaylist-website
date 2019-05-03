using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Business.Audio;

namespace NPlaylist.Business.LocalRepository
{
    public interface IAudioLocalRepository
    {
        Task AddAsync(AudioUploadDto audioLocalStoreModel, CancellationToken ct);
        void Delete(string filePath);
    }
}