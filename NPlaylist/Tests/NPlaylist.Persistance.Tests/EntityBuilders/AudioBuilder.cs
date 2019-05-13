using System;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.Tests.EntityBuilders
{
    public class AudioBuilder
    {
        private Guid _audioId;
        private AudioMeta _meta;

        public AudioBuilder()
        {
            _audioId = GuidFactory.MakeFromInt(42);
        }

        public Audio Build()
        {
            return new Audio
            {
                AudioId = _audioId,
                Meta = _meta
            };
        }

        public AudioBuilder WithId(Guid audioId)
        {
            _audioId = audioId;
            return this;
        }

        public AudioBuilder WithMeta(AudioMeta audioMeta)
        {
            _meta = audioMeta;
            return this;
        }
    }
}
