using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;
using System;

namespace NPlaylist.Persistence.PlaylistEntries
{
    public class PlaylistEntriesRepository : SqlCrudRepository<Playlist, Guid>, IPlaylistEntriesRepository
    {
        public PlaylistEntriesRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
