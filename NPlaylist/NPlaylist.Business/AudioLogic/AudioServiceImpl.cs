using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Business.LocalRepository;
using NPlaylist.Business.MetaTags;
using NPlaylist.Business.Providers;
using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.AudioMetaEntries;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Business.AudioLogic
{
    public class AudioServiceImpl : IAudioService
    {
        private readonly IAudioEntriesRepository _audioEntriesRepository;
        private readonly IAudioMetaEntriesRepository _audioMetaEntriesRepository;
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly IAudioLocalRepository _localAudioRepository;
        private readonly IPathProvider _pathProvider;
        private readonly ITagsProvider _tagsProvider;

        public AudioServiceImpl(
            IPathProvider pathProvider,
            IAudioLocalRepository localAudioRepository,
            IAudioEntriesRepository audioEntriesRepository,
            ITagsProvider tagsProvider,
            IDateTimeWrapper dateTimeWrapper,
            IAudioMetaEntriesRepository audioMetaEntriesRepository)
        {
            _pathProvider = pathProvider;
            _localAudioRepository = localAudioRepository;
            _audioEntriesRepository = audioEntriesRepository;
            _tagsProvider = tagsProvider;
            _dateTimeWrapper = dateTimeWrapper;
            _audioMetaEntriesRepository = audioMetaEntriesRepository;
        }

        public async Task DeleteAudioAsync(Guid id, CancellationToken ct)
        {
            var audio = await _audioEntriesRepository.GetAudioIncludingMetaAsync(id, ct);

            try
            {
                RemoveAudioFileFromLocal(id);
                await RemoveAudioFromDb(ct, audio);
                await RemoveAudioTagsFromDb(ct, audio);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Operation failed.");
            }
        }

        public Task<Audio> GetAudioAsync(Guid id, CancellationToken ct)
        {
            return _audioEntriesRepository.GetAudioIncludingMetaAsync(id, ct);
        }

        public async Task<AudioPaginationDto> GetAudioPaginationAsync(int pageIndex, int totalEntriesOnPage, CancellationToken ct)
        {
            if (pageIndex < 1 || totalEntriesOnPage < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var totalEntriesCount = await _audioEntriesRepository.CountAsync(ct);
            var entries = await _audioEntriesRepository.GetRangeAsync((pageIndex - 1) * totalEntriesOnPage, totalEntriesOnPage, ct);

            var totalPagesCount = (int)Math.Ceiling(totalEntriesCount / (double)totalEntriesOnPage);

            var dto = new AudioPaginationDto
            {
                Items = entries.ToList(),
                PageIndex = pageIndex,
                TotalNbOfPages = totalPagesCount
            };

            return dto;
        }

        public async Task UpdateAudioAsync(Audio audio, CancellationToken ct)
        {
            try
            {
                _audioEntriesRepository.Update(audio);
                await _audioEntriesRepository.SaveAsync(ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _audioEntriesRepository.EntryExistsAsync(audio.AudioId, ct))
                {
                    throw new Exception("Operation failed");
                }

                throw new KeyNotFoundException();
            }
        }

        public async Task UploadAudioAsync(AudioUploadDto uploadDto, CancellationToken ct)
        {
            uploadDto.Path = _pathProvider.BuildPath(uploadDto.File.FileName);
            await SaveToLocalFolder(uploadDto, ct);
            await SaveIntoDatabase(uploadDto, ct);
        }

        private void RemoveAudioFileFromLocal(Guid id)
        {
            var audioPath = _audioEntriesRepository.GetById(id).Path;
            _localAudioRepository.Delete(audioPath);
        }

        private async Task RemoveAudioFromDb(CancellationToken ct, Audio audio)
        {
            _audioEntriesRepository.Remove(audio);
            await _audioEntriesRepository.SaveAsync(ct);
        }

        private async Task RemoveAudioTagsFromDb(CancellationToken ct, Audio audio)
        {
            _audioMetaEntriesRepository.Remove(audio.Meta);
            await _audioMetaEntriesRepository.SaveAsync(ct);
        }

        private async Task SaveIntoDatabase(AudioUploadDto uploadDto, CancellationToken ct)
        {
            var audioModel = new Audio
            {
                OwnerId = uploadDto.PublisherId,
                Path = uploadDto.Path,
                Meta = _tagsProvider.GetTags(uploadDto.Path),
                UtcCreatedTime = _dateTimeWrapper.UtcNow
            };

            await _audioEntriesRepository.AddAsync(audioModel, ct);
            await _audioEntriesRepository.SaveAsync(ct);
        }

        private async Task SaveToLocalFolder(AudioUploadDto uploadDto, CancellationToken ct)
        {
            await _localAudioRepository.AddAsync(uploadDto, ct);
        }
    }
}