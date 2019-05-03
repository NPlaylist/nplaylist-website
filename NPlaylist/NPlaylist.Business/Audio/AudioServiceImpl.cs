using NPlaylist.Business.LocalRepository;
using NPlaylist.Business.MetaTags;
using NPlaylist.Business.Providers;
using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.AudioEntries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Business.Audio
{
    public class AudioServiceImpl : IAudioService
    {
        private readonly IAudioEntriesRepository _audioEntriesRepository;
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly IAudioLocalRepository _localAudioRepository;
        private readonly IPathProvider _pathProvider;
        private readonly ITagsProvider _tagsProvider;

        public AudioServiceImpl(
            IPathProvider pathProvider,
            IAudioLocalRepository localAudioRepository,
            IAudioEntriesRepository audioEntriesRepository,
            ITagsProvider tagsProvider,
            IDateTimeWrapper dateTimeWrapper)
        {
            _pathProvider = pathProvider;
            _localAudioRepository = localAudioRepository;
            _audioEntriesRepository = audioEntriesRepository;
            _tagsProvider = tagsProvider;
            _dateTimeWrapper = dateTimeWrapper;
        }

        public async Task UploadAsync(AudioUploadDto uploadViewModel, CancellationToken ct)
        {
            var path = _pathProvider.BuildPath(uploadViewModel.File.FileName);
            await SaveToLocalFolder(uploadViewModel, ct, path);
            await SaveIntoDatabase(uploadViewModel, ct, path);
        }

        private async Task SaveIntoDatabase(AudioUploadDto uploadViewModel, CancellationToken ct, string path)
        {
            var audioModel = new Persistence.DbModels.Audio
            {
                OwnerId = uploadViewModel.PublisherId,
                Path = path,
                Meta = _tagsProvider.GetTags(path),
                UtcCreatedTime = _dateTimeWrapper.UtcNow
            };

            _audioEntriesRepository.Add(audioModel);
            await _audioEntriesRepository.SaveAsync(ct);
        }

        private async Task SaveToLocalFolder(AudioUploadDto uploadViewModel, CancellationToken ct, string path)
        {
            var localStoreModel = new AudioUploadDto
            {
                File = uploadViewModel.File,
                Path = path
            };

            await _localAudioRepository.AddAsync(localStoreModel, ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct)
        {
            var audioPath = _audioEntriesRepository.GetById(id).Path;
            _localAudioRepository.Delete(audioPath);
        }
    }
}