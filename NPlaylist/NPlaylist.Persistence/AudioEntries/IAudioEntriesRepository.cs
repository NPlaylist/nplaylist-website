using System;
using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.AudioEntries
{
    public interface IAudioEntriesRepository : ICrudRepository<Audio, Guid>
    {
    }
}
