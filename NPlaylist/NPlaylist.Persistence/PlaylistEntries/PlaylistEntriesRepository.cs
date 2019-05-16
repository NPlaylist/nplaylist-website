using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NPlaylist.Persistence.PlaylistEntries
{
    public class PlaylistEntriesRepository : SqlCrudRepository<Playlist, Guid>, IPlaylistEntriesRepository
    {
        public PlaylistEntriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> CountAsync(CancellationToken ct)
        {
            return await _dbSet.CountAsync(ct);
        }

        public async Task<IEnumerable<Playlist>> GetRangeAsync(int start, int count, CancellationToken ct)
        {
            return await _dbSet
                .OrderByDescending(e => e.UtcDateTime)
                .Skip(start)
                .Take(count)
                .Include(e => e.AudioPlaylists)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
