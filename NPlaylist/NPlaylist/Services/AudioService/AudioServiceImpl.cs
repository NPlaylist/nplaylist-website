using System;
using System.Collections.Generic;
using NPlaylist.ViewModels;
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

        public async Task<IEnumerable<AudioEntryViewModel>> GetAudioEntriesAsync(CancellationToken ct)
        {
            var entriesRepo = await _audioEntriesRepository.GetAllAsync(ct);
            return _mapper.Map<List<AudioEntryViewModel>>(entriesRepo);
        }
    
        public Task<Audio> GetAudioAsync(Guid id, CancellationToken ct)
        {
            return _audioEntriesRepository.GetAudioIncludingMetaAsync(id, ct);
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
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }
    }
}