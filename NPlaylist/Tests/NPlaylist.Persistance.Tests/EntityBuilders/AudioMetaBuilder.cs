using System;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Persistence.Tests.EntityBuilders
{
    public class AudioMetaBuilder
    {
        private Guid _audioMetaId = new Guid();
        private string _title = "Foo Title";
        private string _author = "Foo Author";
        private string _album = "Foo Album";

        public AudioMeta Build()
        {
            return new AudioMeta
            {
                AudioMetaId = _audioMetaId,
                Title = _title,
                Author = _author,
                Album = _album
            };
        }
    }
}
