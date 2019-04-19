using NPlaylist.DTOs;
using NPlaylist.Managers.PathProvider;
using NPlaylist.Managers.TagProvider;
using NPlaylist.Models;
using NPlaylist.Models.Entity;
using NPlaylist.Repositories.AudioRepository;
using NPlaylist.Repositories.LocalAudioRepository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Services.AudioService
{
    public class AudioService : IAudioService
    {
        private readonly IAudioLocalRepo _audioLocalRepo;
        private readonly IAudioTagsProvider _audioTagsProvider;
        private readonly IPathProvider _pathProvider;
        private readonly IAudioDbRepo _audioDbRepo;

        public AudioService(
            IAudioLocalRepo audioLocalRepo,
            IAudioTagsProvider audioTagsProvider,
            IPathProvider pathProvider,
            IAudioDbRepo audioDbRepo)
        {
            _audioDbRepo = audioDbRepo;
            _audioLocalRepo = audioLocalRepo;
            _audioTagsProvider = audioTagsProvider;
            _pathProvider = pathProvider;
        }

        public async Task Upload(UploadedFileDto uploadedFile, CancellationToken ct)
        {
            var path = _pathProvider.BuildPath(uploadedFile.FormFile.FileName);

            var audioLocalStoreModel = new AudioLocalStoreModel
            {
                FormFile = uploadedFile.FormFile,
                Path = path
            };

            await _audioLocalRepo.StoreAsync(audioLocalStoreModel, ct);

            var audio = new Audio
            {
                Meta = _audioTagsProvider.GetTags(path),
                Path = path,
                UserId = Guid.Parse(uploadedFile.UserId)
            };

            _audioDbRepo.Add(audio);
            await _audioDbRepo.SaveAsync(ct);
        }
    }
}
