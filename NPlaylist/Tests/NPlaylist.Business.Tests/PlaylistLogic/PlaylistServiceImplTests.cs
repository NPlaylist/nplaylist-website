using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NPlaylist.Authentication.Users;
using NPlaylist.Business.PlaylistLogic;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.DbModels;
using NPlaylist.Persistence.PlaylistEntries;
using NPlaylist.Persistence.Tests;
using NPlaylist.Persistence.Tests.EntityBuilders;
using NPlaylist.Tests;
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

            var actual = await sut.GetPlaylistPaginationAsync(1, 1, CancellationToken.None);
            actual.Should().NotBeNull();
        }

        [Fact]
        public async Task GetPlaylistDetails_ForNonexistentPlaylist_ReturnsNull()
        {
            var playlistRepo = Substitute.For<IPlaylistEntriesRepository>();
            playlistRepo
                .GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None)
                .Returns(x => (Playlist)null);

            var sut = new PlaylistServiceBuilder()
                .WithPlaylistRepository(playlistRepo)
                .Build();

            var actual = await sut.GetPlaylistDetails(new Guid(), CancellationToken.None);
            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetPlaylistDetails_ForExistingOwner_DetailsContainsOwnerUsername()
        {
            var playlistOwner = new IdentityUser(userName: "Foo");

            var userRepo = Substitute.For<IUserRepository>();
            userRepo
                .FindByIdAsync(Arg.Any<string>(), CancellationToken.None)
                .Returns(x => playlistOwner);

            var playlistRepo = Substitute.For<IPlaylistEntriesRepository>();
            playlistRepo
                .GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None)
                .Returns(x => new PlaylistBuilder().Build());

            var sut = new PlaylistServiceBuilder()
                .WithPlaylistRepository(playlistRepo)
                .WithUserRepository(userRepo)
                .WithMapper(new MapperBuilder().WithDefaultProfile().Build())
                .Build();

            var playlistDetails = await sut.GetPlaylistDetails(new Guid(), CancellationToken.None);
            var actual = playlistDetails.OwnerUsername;
            actual.Should().Be("Foo");
        }

        [Fact]
        public async Task GetPlaylistDetails_ForPlaylistContainingAnAudio_DetailsContainsASingleAudio()
        {
            var playlist = new PlaylistBuilder()
                .WithId(GuidFactory.MakeFromInt(42))
                .Build();

            var playlistRepo = Substitute.For<IPlaylistEntriesRepository>();
            playlistRepo
                .GetByIdAsync(Arg.Any<Guid>(), CancellationToken.None)
                .Returns(x => playlist);

            var audioRepo = Substitute.For<IAudioEntriesRepository>();
            audioRepo
                .GetAudioEntriesByPlaylistAsync(playlist.PlaylistId, CancellationToken.None)
                .Returns(x => new[] { new AudioBuilder().Build() });

            var sut = new PlaylistServiceBuilder()
                .WithPlaylistRepository(playlistRepo)
                .WithAudioRepository(audioRepo)
                .WithMapper(new MapperBuilder().WithDefaultProfile().Build())
                .Build();

            var playlistDetails = await sut.GetPlaylistDetails(playlist.PlaylistId, CancellationToken.None);
            var actual = playlistDetails.AudioEntries;
            actual.Should().ContainSingle();
        }
    }
}