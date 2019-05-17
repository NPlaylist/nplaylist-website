using System;
using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.PlaylistEntries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Authentication.Users;

namespace NPlaylist.Business.PlaylistLogic
{
    public class PlaylistServiceImpl : IPlaylistService
    {
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly IMapper _mapper;
        private readonly IPlaylistEntriesRepository _playlistRepository;
        private readonly IAudioEntriesRepository _audioRepository;
        private readonly IUserRepository _userRepository;

        public PlaylistServiceImpl(
            IDateTimeWrapper dateTimeWrapper,
            IMapper mapper,
            IPlaylistEntriesRepository playlistRepository,
            IAudioEntriesRepository audioRepository,
            IUserRepository userRepository)
        {
            _dateTimeWrapper = dateTimeWrapper;
            _mapper = mapper;
            _playlistRepository = playlistRepository;
            _audioRepository = audioRepository;
            _userRepository = userRepository;
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

        public async Task<PlaylistDetailsDto> GetPlaylistDetails(Guid playlistId, CancellationToken ct)
        {
            var playlist = await _playlistRepository.GetByIdAsync(playlistId, ct);
            if (playlist == null)
            {
                return null;
            }

            var audios = await _audioRepository.GetAudioEntriesByPlaylistAsync(playlistId, ct);
            var owner = await _userRepository.FindByIdAsync(playlist.OwnerId.ToString(), ct);

            var playlistDto = _mapper.Map<PlaylistDetailsDto>(playlist);
            playlistDto.AudioEntries = audios;
            playlistDto.OwnerUsername = owner?.UserName;

            return playlistDto;
        }
    }
}
