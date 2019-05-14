using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;
using System;

namespace NPlaylist.Persistence.PlaylistEntries
{
    public class PlaylistEntriesRepository : SqlCrudRepository<Playlist, Guid>, IPlaylistEntriesRepository
    {
        public PlaylistEntriesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
