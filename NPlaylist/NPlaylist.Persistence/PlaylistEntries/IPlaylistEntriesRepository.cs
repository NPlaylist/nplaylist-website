using NPlaylist.Persistence.CrudRepository;
using NPlaylist.Persistence.DbModels;
using System;

namespace NPlaylist.Persistence.PlaylistEntries
{
    public interface IPlaylistEntriesRepository : ICrudRepository<Playlist, Guid>
    {
    }
}
