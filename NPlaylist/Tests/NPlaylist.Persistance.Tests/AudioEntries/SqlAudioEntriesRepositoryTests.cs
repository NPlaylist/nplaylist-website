using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCoreMock.NSubstitute;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.Tests.EntityBuilders;
using Xunit;

namespace NPlaylist.Persistence.Tests.AudioEntries
{
    public class SqlAudioEntriesRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> DummyDbOptions =>
            new DbContextOptionsBuilder<ApplicationDbContext>().Options;

        [Fact]
        public async Task EntryExistsAsync_ForNonexistentElement_ReturnsFalse()
        {
            var audio = new AudioBuilder().WithId(GuidFactory.MakeFromInt(0)).Build();

            var dbContextMock = new DbContextMock<ApplicationDbContext>(DummyDbOptions);
            var audioEntriesDbSetMock = dbContextMock.CreateDbSetMock(x => x.AudioEntries, new[] { audio });

            var sut = new SqlAudioEntriesRepository(dbContextMock.Object);

            var actual = await sut.EntryExistsAsync(GuidFactory.MakeFromInt(1), CancellationToken.None);
            actual.Should().BeFalse();
        }

        [Fact]
        public async Task EntryExistsAsync_ForExistingElement_ReturnsTrue()
        {
            var audio = new AudioBuilder().Build();

            var dbContextMock = new DbContextMock<ApplicationDbContext>(DummyDbOptions);
            var audioEntriesDbSetMock = dbContextMock.CreateDbSetMock(x => x.AudioEntries, new[] { audio });

            var sut = new SqlAudioEntriesRepository(dbContextMock.Object);

            var actual = await sut.EntryExistsAsync(audio.AudioId, CancellationToken.None);
            actual.Should().BeTrue();
        }

        [Fact]
        public async Task CountAsync_ForMultipleEntries_CountAsExpected()
        {
            var audios = new[]
            {
                new AudioBuilder().WithId(GuidFactory.MakeFromInt(0)).Build(),
                new AudioBuilder().WithId(GuidFactory.MakeFromInt(1)).Build(),
            };

            var dbContextMock = new DbContextMock<ApplicationDbContext>(DummyDbOptions);
            var audioEntriesDbSetMock = dbContextMock.CreateDbSetMock(x => x.AudioEntries, audios);

            var sut = new SqlAudioEntriesRepository(dbContextMock.Object);

            var actual = await sut.CountAsync(CancellationToken.None);
            actual.Should().Be(2);
        }

        [Fact]
        public async Task GetRangeAsync_ReturnsExpectedNbOfElemnets()
        {
            var audios = new[]
            {
                new AudioBuilder().WithId(GuidFactory.MakeFromInt(0)).Build(),
                new AudioBuilder().WithId(GuidFactory.MakeFromInt(1)).Build(),
                new AudioBuilder().WithId(GuidFactory.MakeFromInt(2)).Build(),
                new AudioBuilder().WithId(GuidFactory.MakeFromInt(3)).Build(),
            };

            var dbContextMock = new DbContextMock<ApplicationDbContext>(DummyDbOptions);
            var audioEntriesDbSetMock = dbContextMock.CreateDbSetMock(x => x.AudioEntries, audios);

            var sut = new SqlAudioEntriesRepository(dbContextMock.Object);

            var actual = await sut.GetRangeAsync(1, 2, CancellationToken.None);
            actual.Should().HaveCount(2);
        }
    }
}
