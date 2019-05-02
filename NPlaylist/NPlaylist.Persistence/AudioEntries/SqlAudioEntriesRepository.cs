using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.AudioEntries
{
    public class SqlAudioEntriesRepository : SqlCrudRepository<Audio, Guid>, IAudioEntriesRepository
    {
        public SqlAudioEntriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> EntryExistsAsync(Guid audioId, CancellationToken ct)
        {
            var entity = await GetByIdAsync(audioId, ct);
            return entity != null;
        }

        public Task<Audio> GetAudioIncludingMetaAsync(Guid audioId, CancellationToken ct)
        {
            return _dbSet
                .Include(x => x.Meta)
                .FirstOrDefaultAsync(x => x.AudioId == audioId, ct);
        }
    }
}
