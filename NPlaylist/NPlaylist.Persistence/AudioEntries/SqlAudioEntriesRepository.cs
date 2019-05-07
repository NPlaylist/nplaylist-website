using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NPlaylist.Persistence.AudioEntries
{
    public class SqlAudioEntriesRepository : SqlCrudRepository<Audio, Guid>, IAudioEntriesRepository
    {
        public SqlAudioEntriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> EntryExistsAsync(Guid audioId, CancellationToken ct)
        {
            return _dbSet.AnyAsync(x => x.AudioId == audioId, ct);
        }

        public Task<Audio> GetAudioIncludingMetaAsync(Guid audioId, CancellationToken ct)
        {
            return _dbSet
                .Include(x => x.Meta)
                .FirstOrDefaultAsync(x => x.AudioId == audioId, ct);
        }

        public async Task<int> CountAsync(CancellationToken ct)
        {
            return await _dbSet.CountAsync(ct);
        }

        public async Task<IEnumerable<Audio>> GetRangeAsync(int start, int count, CancellationToken ct)
        {
            return await _dbSet
                .Include(e => e.Meta)
                .Skip(start)
                .Take(count)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}