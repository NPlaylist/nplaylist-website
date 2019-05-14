using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Persistence.PlaylistEntries
{
    public interface IPlaylistEntriesRepository : ICrudRepository<Playlist, Guid>
    {
        Task<int> CountAsync(CancellationToken ct);
        Task<IEnumerable<Playlist>> GetRangeAsync(int start, int count, CancellationToken ct);
    }
}
