using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Business.PlaylistLogic
{
    public interface IPlaylistService
    {
        Task CreatePlaylist(PlaylistCreateDto playlistDto, CancellationToken ct);
    }
}