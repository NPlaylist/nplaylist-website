using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;
using System;

namespace NPlaylist.Persistence.AudioEntries
{
	public class SqlAudioEntriesRepository : SqlCrudRepository<Audio, Guid>, IAudioEntriesRepository
    {
        public SqlAudioEntriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
