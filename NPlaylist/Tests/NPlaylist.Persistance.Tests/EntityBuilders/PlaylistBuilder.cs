using NPlaylist.Persistence.DbModels;
using System;

namespace NPlaylist.Persistence.Tests.EntityBuilders
{
    public class PlaylistBuilder
    {
        private Guid _playlistId;

        public PlaylistBuilder()
        {
            _playlistId = new Guid("00000000-0000-0000-0000-000000000001");
        }

        public Playlist Build()
        {
            return new Playlist
            {
                PlaylistId = _playlistId
            };
        }

        public PlaylistBuilder WithId(Guid playlistId)
        {
            _playlistId = playlistId;

            return this;
        }
    }
}
