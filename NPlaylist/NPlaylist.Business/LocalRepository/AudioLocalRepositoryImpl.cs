using NPlaylist.Business.Audio;
using NPlaylist.Infrastructure.System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Business.LocalRepository
{
    public class AudioLocalRepositoryImpl : IAudioLocalRepository
    {
        private readonly IDirectoryWrapper _directoryWrapper;
        private readonly IFileStreamFactory _streamFactory;
        private readonly IPathWrapper _pathWrapper;

        public AudioLocalRepositoryImpl(IPathWrapper pathWrapper, IDirectoryWrapper directoryWrapper, IFileStreamFactory streamFactory)
        {
            _directoryWrapper = directoryWrapper;
            _streamFactory = streamFactory;
            _pathWrapper = pathWrapper;
        }

        public async Task AddAsync(AudioUploadDto audioLocalStoreModel, CancellationToken ct)
        {
            CreateDirectoryIfNotExists(audioLocalStoreModel.Path);

            using (var fileStream = _streamFactory.Create(audioLocalStoreModel.Path, FileMode.Create, FileAccess.Write))
            {
                await audioLocalStoreModel.File.CopyToAsync(fileStream, ct);
            }
        }

        private void CreateDirectoryIfNotExists(string path)
        {
            _directoryWrapper.CreateDirectory(_pathWrapper.GetDirectoryName(path));
        }
    }
}