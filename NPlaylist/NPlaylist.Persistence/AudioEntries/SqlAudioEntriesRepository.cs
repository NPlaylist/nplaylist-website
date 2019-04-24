using System;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.AudioEntries
{
    public class SqlAudioEntriesRepository : SqlCrudRepository<Audio, Guid>, IAudioEntriesRepository
    {
        public SqlAudioEntriesRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
