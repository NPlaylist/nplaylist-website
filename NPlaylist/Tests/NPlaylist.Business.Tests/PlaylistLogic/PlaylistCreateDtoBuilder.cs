using NPlaylist.Business.PlaylistLogic;
using System;

namespace NPlaylist.Business.Tests.PlaylistLogic
{
    public class PlaylistCreateDtoBuilder
    {
        private readonly PlaylistCreateDto _playlistCreateDto;

        public PlaylistCreateDtoBuilder()
        {
            _playlistCreateDto = new PlaylistCreateDto();
        }

        public PlaylistCreateDtoBuilder WithTitle(string title)
        {
            _playlistCreateDto.Title = title;
            return this;
        }

        public PlaylistCreateDtoBuilder WithDescription(string description)
        {
            _playlistCreateDto.Description = description;
            return this;
        }

        public PlaylistCreateDtoBuilder WithOwnerId(Guid ownerId)
        {
            _playlistCreateDto.OwnerId = ownerId;
            return this;
        }

        public PlaylistCreateDto Build()
        {
            return _playlistCreateDto;
        }
    }
}