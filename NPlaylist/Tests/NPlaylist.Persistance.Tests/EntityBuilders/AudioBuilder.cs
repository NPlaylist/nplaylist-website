using System;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.Tests.EntityBuilders
{
    public class AudioBuilder
    {
        private Guid _audioId;

        public AudioBuilder()
        {
            _audioId = new Guid("00000000-0000-0000-0000-000000000001");
        }

        public Audio Build()
        {
            return new Audio
            {
                AudioId = _audioId,
            };
        }

        public AudioBuilder WithId(Guid audioId)
        {
            _audioId = audioId;
            return this;
        }
    }
}
