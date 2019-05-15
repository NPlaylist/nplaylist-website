using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.PlaylistEntries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using System;
using NPlaylist.Persistence.AudioEntries;

namespace NPlaylist.Business.PlaylistLogic
{
    public class PlaylistServiceImpl : IPlaylistService
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly IMapper _mapper;
        private readonly IPlaylistEntriesRepository _playlistRepository;
        private readonly IAudioEntriesRepository _audioRepository;

        public PlaylistServiceImpl(
            IDateTimeWrapper dateTimeWrapper,
            IMapper mapper,
            IPlaylistEntriesRepository playlistRepository,
            IAudioEntriesRepository audioRepository)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _mapper = mapper;
            _playlistRepository = playlistRepository;
            _audioRepository = audioRepository;
        }

        public async Task CreatePlaylist(PlaylistCreateDto playlistDto, CancellationToken ct)
        {
            var playlist = _mapper.Map<Playlist>(playlistDto);
            playlist.UtcDateTime = _dateTimeWrapper.UtcNow;
            playlist.AudioPlaylists = new List<AudioPlaylists>();

            await _playlistRepository.AddAsync(playlist, ct);
            await _playlistRepository.SaveAsync(ct);
        }

        public Task<Playlist> GetPlaylistAsync(Guid playlistId, CancellationToken ct)
        {
            return _playlistRepository.GetByIdAsync(playlistId, ct);
        }

        public Task<IEnumerable<Audio>> GetAudiosByPlaylistAsync(Guid playlistId, CancellationToken ct)
        {
            return _audioRepository.GetAudioEntriesByPlaylistAsync(playlistId, ct);
        }
    }
}