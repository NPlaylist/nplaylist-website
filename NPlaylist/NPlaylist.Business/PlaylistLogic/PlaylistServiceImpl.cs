using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.PlaylistEntries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

namespace NPlaylist.Business.PlaylistLogic
{
    public class PlaylistServiceImpl : IPlaylistService
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly IMapper _mapper;
        private readonly IPlaylistEntriesRepository _playlistRepository;

        public PlaylistServiceImpl(
            IDateTimeWrapper dateTimeWrapper,
            IMapper mapper,
            IPlaylistEntriesRepository playlistRepository)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _mapper = mapper;
            _playlistRepository = playlistRepository;
        }

        public async Task CreatePlaylist(PlaylistCreateDto playlistDto, CancellationToken ct)
        {
            var playlist = _mapper.Map<Playlist>(playlistDto);
            playlist.UtcDateTime = _dateTimeWrapper.UtcNow;
            playlist.AudioPlaylists = new List<AudioPlaylists>();

            await _playlistRepository.AddAsync(playlist, ct);
            await _playlistRepository.SaveAsync(ct);
        }
    }
}