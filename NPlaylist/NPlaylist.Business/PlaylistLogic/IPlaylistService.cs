using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Business.PlaylistLogic
{
    public interface IPlaylistService
    {
        Task CreatePlaylist(PlaylistCreateDto playlistDto, CancellationToken ct);
        Task<PlaylistPaginationDto> GetPlaylistPaginationAsync(int page, int count, CancellationToken ct);
        Task<IEnumerable<Audio>> GetAudiosByPlaylistAsync(Guid playlistId, CancellationToken ct);
        Task<Playlist> GetPlaylistAsync(Guid playlistId, CancellationToken ct);
    }
}