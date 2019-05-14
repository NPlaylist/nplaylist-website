using System;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.Tests.EntityBuilders
{
    public class AudioPlaylistsBuilder
    {
        private Guid _audioId;
        private Guid _playlistId;

        public AudioPlaylists Build()
        {
            return new AudioPlaylists
            {
                AudioId = _audioId,
                PlaylistId = _playlistId
            };
        }

        public AudioPlaylistsBuilder WithAudioId(int audioId)
        {
            _audioId = GuidFactory.MakeFromInt(audioId);
            return this;
        }

        public AudioPlaylistsBuilder WithPlaylistId(int playlistId)
        {
            _playlistId = GuidFactory.MakeFromInt(playlistId);
            return this;
        }
    }
}
