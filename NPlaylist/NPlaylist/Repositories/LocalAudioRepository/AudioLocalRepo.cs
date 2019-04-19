using NPlaylist.Models;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Wrappers.DirectoryWrapper;
using NPlaylist.Wrappers.PathWrapper;

namespace NPlaylist.Repositories.LocalAudioRepository
{
    public class AudioLocalRepo : IAudioLocalRepo
    {
        private readonly IPathWrapper _pathWrapper;
        private IDirectoryWrapper _directoryWrapper;

        public AudioLocalRepo(IPathWrapper pathWrapper, IDirectoryWrapper directoryWrapper)
        {
            _directoryWrapper = directoryWrapper;
            _pathWrapper = pathWrapper;
        }

        public async Task StoreAsync(AudioLocalStoreModel audioLocalStoreModel, CancellationToken ct)
        {
            CreateDirectoryIfNotExists(audioLocalStoreModel.Path);

            using (var fileStream = new FileStream(audioLocalStoreModel.Path, FileMode.Create, FileAccess.Write))
            {
                await audioLocalStoreModel.FormFile.CopyToAsync(fileStream, ct);
            }
        }

        private void CreateDirectoryIfNotExists(string path)
        {
            _directoryWrapper.CreateDirectory(_pathWrapper.GetDirectoryName(path));
        }
    }
}

