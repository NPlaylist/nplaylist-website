using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Business.AudioLogic;

namespace NPlaylist.Business.LocalRepository
{
    public interface IAudioLocalRepository
    {
        Task AddAsync(AudioUploadDto audioLocalStoreModel, CancellationToken ct);
        void Delete(string filePath);
    }
}