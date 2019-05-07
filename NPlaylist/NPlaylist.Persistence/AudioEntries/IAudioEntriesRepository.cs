using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.AudioEntries
{
    public interface IAudioEntriesRepository : ICrudRepository<Audio, Guid>
    {
        Task<bool> EntryExistsAsync(Guid audioId, CancellationToken ct);
        Task<Audio> GetAudioIncludingMetaAsync(Guid audioId, CancellationToken ct);
        Task<int> CountAsync(CancellationToken ct);
        Task<IEnumerable<Audio>> GetRangeAsync(int start, int count, CancellationToken ct);
    }
}
