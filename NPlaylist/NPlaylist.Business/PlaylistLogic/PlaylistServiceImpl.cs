using System;
using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.PlaylistEntries;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PlaylistPaginationDto> GetPlaylistPaginationAsync(int pageIndex, int totalEntriesOnPage, CancellationToken ct)
        {
            if (pageIndex < 1 || totalEntriesOnPage < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            var totalEntriesCount = await _playlistRepository.CountAsync(ct);
            var entries = await _playlistRepository.GetRangeAsync((pageIndex - 1) * totalEntriesOnPage, totalEntriesOnPage, ct);

            var totalPagesCount = (int)Math.Ceiling(totalEntriesCount / (double)totalEntriesOnPage);

            var dto = new PlaylistPaginationDto()
            {
                Items = entries.ToList(),
                PageIndex = pageIndex,
                TotalNbOfPages = totalPagesCount
            };

            return dto;
        }
    }
}
