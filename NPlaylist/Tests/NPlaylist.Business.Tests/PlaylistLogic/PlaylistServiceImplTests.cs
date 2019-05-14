using AutoMapper;
using FluentAssertions;
using NPlaylist.Business.PlaylistLogic;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.PlaylistEntries;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Business.Tests.PlaylistLogic
{
    public class PlaylistServiceImplTests
    {
        [Fact]
        public async Task CreatePlaylist_MapperMapIsCalled_True()
        {
            var mapperMock = Substitute.For<IMapper>();
            mapperMock.Map<Playlist>(Arg.Any<PlaylistCreateDto>()).Returns(new Playlist());
            var sut = new PlaylistServiceBuilder()
                .WithMapper(mapperMock)
                .Build();
            var playlistDto = new PlaylistCreateDtoBuilder().Build();
            await sut.CreatePlaylist(playlistDto, CancellationToken.None);
            mapperMock.Received().Map<Playlist>(Arg.Any<PlaylistCreateDto>());
        }

        [Fact]
        public async Task CreatePlaylist_AddAsyncIsCalled_True()
        {
            var playlistRepositoryMock = Substitute.For<IPlaylistEntriesRepository>();
            var mapperMock = Substitute.For<IMapper>();
            mapperMock.Map<Playlist>(Arg.Any<PlaylistCreateDto>()).Returns(new Playlist());
            var sut = new PlaylistServiceBuilder()
                .WithPlaylistRepository(playlistRepositoryMock)
                .WithMapper(mapperMock)
                .Build();
            var playlistDto = new PlaylistCreateDtoBuilder().Build();
            await sut.CreatePlaylist(playlistDto, CancellationToken.None);
            await playlistRepositoryMock.Received().AddAsync(Arg.Any<Playlist>(), CancellationToken.None);
        }

        [Fact]
        public async Task CreatePlaylist_AddAsyncShouldBeCalledWithCorrectArguments_True()
        {
            var playlistRepositoryMock = Substitute.For<IPlaylistEntriesRepository>();
            var playlistDto = new PlaylistCreateDtoBuilder()
                .WithTitle("Test")
                .Build();
            var mapperMock = Substitute.For<IMapper>();
            mapperMock.Map<Playlist>(playlistDto).Returns(new Playlist { Title = playlistDto.Title });

            var sut = new PlaylistServiceBuilder()
                .WithMapper(mapperMock)
                .WithPlaylistRepository(playlistRepositoryMock)
                .Build();

            var expectedParameter = mapperMock.Map<Playlist>(playlistDto);
            await sut.CreatePlaylist(playlistDto, CancellationToken.None);
            await playlistRepositoryMock.Received().AddAsync(expectedParameter, CancellationToken.None);
        }

        [Fact]
        public void GetPlaylistPaginationAsync_ReturnsOutOfRangeException()
        {
            var sut = new PlaylistServiceBuilder()
                .Build();

            Func<Task> action = async ()
                => await sut.GetPlaylistPaginationAsync(0, 42, CancellationToken.None);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public async Task GetPlaylistPaginationAsync_CallsAudioRepository()
        {
            var repository = Substitute.For<IPlaylistEntriesRepository>();
            var sut = new PlaylistServiceBuilder()
                .WithPlaylistRepository(repository)
                .Build();

            await sut.GetPlaylistPaginationAsync(2, 1, CancellationToken.None);
            await repository.Received().GetRangeAsync(1, 1, CancellationToken.None);
        }

        [Fact]
        public async Task GetPlaylistPaginationAsync_ReturnsNonNull()
        {
            var repository = Substitute.For<IPlaylistEntriesRepository>();
            var sut = new PlaylistServiceBuilder()
                .WithPlaylistRepository(repository)
                .Build();

            var expectedValue = await sut.GetPlaylistPaginationAsync(1, 1, CancellationToken.None);
            expectedValue.Should().NotBeNull();
        }
    }
}