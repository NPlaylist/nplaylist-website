using NPlaylist.Persistence.DbModels;
using System;
using System.Collections.Generic;

namespace NPlaylist.Persistence.Tests.EntityBuilders
{
    public class AudioBuilder
    {
        private Guid _audioId;
        private List<AudioPlaylists> _audioPlaylists;

        public AudioBuilder()
        {
            _audioId = new Guid("00000000-0000-0000-0000-000000000001");
            _audioPlaylists = new List<AudioPlaylists>();
        }

        public Audio Build()
        {
            return new Audio
            {
                AudioId = _audioId,
                AudioPlaylists = _audioPlaylists
            };
        }

        public AudioBuilder WithId(Guid audioId)
        {
            _audioId = audioId;
            return this;
        }

        public AudioBuilder WithId(int audioId)
        {
            _audioId = GuidFactory.MakeFromInt(audioId);
            return this;
        }

        public AudioBuilder WithAudioPlaylist(params AudioPlaylists[] audioPlaylists)
        {
            _audioPlaylists.AddRange(audioPlaylists);
            return this;
        }
    }
}