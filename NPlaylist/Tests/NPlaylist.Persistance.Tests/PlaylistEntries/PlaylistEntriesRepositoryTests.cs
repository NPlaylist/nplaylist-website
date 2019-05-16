using EntityFrameworkCoreMock.NSubstitute;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.PlaylistEntries;
using NPlaylist.Persistence.Tests.EntityBuilders;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Persistence.Tests.PlaylistEntries
{
    public class PlaylistEntriesRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> DummyDbOptions =>
            new DbContextOptionsBuilder<ApplicationDbContext>().Options;

        [Fact]
        public async Task CountAsync_ForMultipleEntries_CountAsExpected()
        {
            var playlists = new[]
            {
                new PlaylistBuilder().WithId(GuidFactory.MakeFromInt(0)).Build(),
                new PlaylistBuilder().WithId(GuidFactory.MakeFromInt(1)).Build(),
            };

            var dbContextMock = new DbContextMock<ApplicationDbContext>(DummyDbOptions);
            dbContextMock.CreateDbSetMock(x => x.PlaylistEntries, playlists);

            var sut = new PlaylistEntriesRepository(dbContextMock.Object);

            var actual = await sut.CountAsync(CancellationToken.None);
            actual.Should().Be(2);
        }

        [Fact]
        public async Task GetRangeAsync_ReturnsExpectedNbOfElements()
        {
            var playlists = new[]
            {
                new PlaylistBuilder().WithId(GuidFactory.MakeFromInt(0)).Build(),
                new PlaylistBuilder().WithId(GuidFactory.MakeFromInt(1)).Build(),
                new PlaylistBuilder().WithId(GuidFactory.MakeFromInt(2)).Build(),
                new PlaylistBuilder().WithId(GuidFactory.MakeFromInt(3)).Build(),
            };

            var dbContextMock = new DbContextMock<ApplicationDbContext>(DummyDbOptions);
            dbContextMock.CreateDbSetMock(x => x.PlaylistEntries, playlists);

            var sut = new PlaylistEntriesRepository(dbContextMock.Object);

            var actual = await sut.GetRangeAsync(1, 2, CancellationToken.None);
            actual.Should().HaveCount(2);
        }
    }
}
