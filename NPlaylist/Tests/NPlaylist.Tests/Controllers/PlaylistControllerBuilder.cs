using AutoMapper;
using NPlaylist.Business.PlaylistLogic;
using NPlaylist.Controllers;
using NSubstitute;

namespace NPlaylist.Tests.Controllers
{
    public class PlaylistControllerBuilder
    {
        private IMapper _mapper;
        private IPlaylistService _playlistService;

        public PlaylistControllerBuilder()
        {
            _mapper = Substitute.For<IMapper>();
            _playlistService = Substitute.For<IPlaylistService>();
        }

        public PlaylistControllerBuilder WithMapper(IMapper mapper)
        {
            _mapper = mapper;
            return this;
        }

        public PlaylistControllerBuilder WithPlaylistService(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
            return this;
        }

        public PlaylistController Build()
        {
            return new PlaylistController(_mapper,_playlistService);
        }
    }
}