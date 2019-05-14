using AutoMapper;
using NPlaylist.Business.PlaylistLogic;
using NPlaylist.Infrastructure.System;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.PlaylistEntries;
using NSubstitute;

namespace NPlaylist.Business.Tests.PlaylistLogic
{
    public class PlaylistServiceBuilder
    {
        private IDateTimeWrapper _dateTimeWrapper;
        private IMapper _mapper;
        private IPlaylistEntriesRepository _playlistRepository;
        private IAudioEntriesRepository _audioRepository;

        public PlaylistServiceBuilder()
        {
            _dateTimeWrapper = Substitute.For<IDateTimeWrapper>();
            _mapper = Substitute.For<IMapper>();
            _playlistRepository = Substitute.For<IPlaylistEntriesRepository>();
            _audioRepository = Substitute.For<IAudioEntriesRepository>();
        }

        public PlaylistServiceBuilder WithDateTimeWrapper(IDateTimeWrapper dateTimeWrapper)
        {
            _dateTimeWrapper = dateTimeWrapper;
            return this;
        }

        public PlaylistServiceBuilder WithMapper(IMapper mapper)
        {
            _mapper = mapper;
            return this;
        }

        public PlaylistServiceBuilder WithPlaylistRepository(IPlaylistEntriesRepository playlistEntriesRepository)
        {
            _playlistRepository = playlistEntriesRepository;
            return this;
        }

        public PlaylistServiceImpl Build()
        {
            return new PlaylistServiceImpl(_dateTimeWrapper, _mapper, _playlistRepository, _audioRepository);
        }
    }
}