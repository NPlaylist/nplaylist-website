using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Business.PlaylistLogic
{
    public interface IPlaylistService
    {
        Task CreatePlaylist(PlaylistCreateDto playlistDto, CancellationToken ct);
        Task<PlaylistPaginationDto> GetPlaylistPaginationAsync(int page, int count, CancellationToken ct);
    }
}