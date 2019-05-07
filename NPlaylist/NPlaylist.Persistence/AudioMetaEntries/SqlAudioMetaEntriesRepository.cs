using System;
using System.Collections.Generic;
using System.Text;
using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.AudioMetaEntries
{
    public class SqlAudioMetaEntriesRepository : SqlCrudRepository<AudioMeta, Guid>, IAudioMetaEntriesRepository
    {
        public SqlAudioMetaEntriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
