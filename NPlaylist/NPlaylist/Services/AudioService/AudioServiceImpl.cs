using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Models;
using NPlaylist.Models.Audio;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Services.AudioService
{
    public class AudioServiceImpl : IAudioService
    {
        private readonly IMapper _mapper;
        private readonly IAudioEntriesRepository _audioEntriesRepository;

        public AudioServiceImpl(IMapper mapper, IAudioEntriesRepository audioEntriesRepository)
        {
            _mapper = mapper;
            _audioEntriesRepository = audioEntriesRepository;
        }

        public async Task<AudioViewModel> GetAudioAsync(Guid id, CancellationToken ct)
        {
            var audio = await _audioEntriesRepository.GetAudioIncludingMetaAsync(id, ct);
            if (audio == null)
            {
                throw new KeyNotFoundException();
            }

            return _mapper.Map<AudioViewModel>(audio);
        }

        public async Task UpdateAudioAsync(AudioViewModel audioViewModel, CancellationToken ct)
        {
            var audio = _mapper.Map<Audio>(audioViewModel);

            try
            {
                _audioEntriesRepository.Update(audio);
                await _audioEntriesRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _audioEntriesRepository.EntryExistsAsync(audio.AudioId, ct))
                {
                    throw new Exception("Operation failed");
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        public async Task<IEnumerable<AudioEntryViewModel>> GetAudioEntriesAsync(CancellationToken ct)
        {
            var entriesRepo = await _audioEntriesRepository.GetAllAsync(ct);
            return _mapper.Map<List<AudioEntryViewModel>>(entriesRepo);
        }
    }
}
