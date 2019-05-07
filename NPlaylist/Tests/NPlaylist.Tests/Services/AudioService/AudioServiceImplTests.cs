using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.DbModels;
using NSubstitute;
using Xunit;

namespace NPlaylist.Tests.Services.AudioService
{
    public class AudioServiceImplTests
    {
        [Fact]
        public async Task GetAudioAsync_ForNoAudios_ReturnsNull()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceImplBuilder().WithAudioRepo(repo).Build();

            var actual = await sut.GetAudioAsync(Guid.Empty, CancellationToken.None);
            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetAudioAsync_ForExistingAudio_ReturnsNonNull()
        {
            var audio = new Audio { AudioId = Guid.NewGuid() };
            var repo = Substitute.For<IAudioEntriesRepository>();
            repo.GetAudioIncludingMetaAsync(audio.AudioId, Arg.Any<CancellationToken>())
                .Returns(audio);

            var mapper = new MapperBuilder().WithDefaultProfile().Build();

            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repo)
                .WithMapper(mapper)
                .Build();

            var actual = await sut.GetAudioAsync(audio.AudioId, CancellationToken.None);
            actual.Should().NotBeNull();
        }

        [Fact]
        public void UpdateAudioAsync_FailedForNonexistentId_ThrowsNotFoundException()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            repo.When(x => x.Update(Arg.Any<Audio>()))
                .Do(x => throw new DbUpdateConcurrencyException("", new List<IUpdateEntry> { null }));

            var mapper = new MapperBuilder().WithDefaultProfile().Build();

            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repo)
                .WithMapper(mapper)
                .Build();

            Func<Task> actualAction = async () =>
                await sut.UpdateAudioAsync(new Audio(), CancellationToken.None);

            actualAction.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateAudioAsync_ForExistingAudio_UpdatesAsExpected()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            var mapper = new MapperBuilder().WithDefaultProfile().Build();
            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repo)
                .WithMapper(mapper)
                .Build();

            await sut.UpdateAudioAsync(new Audio(), CancellationToken.None);

            repo.Received().Update(Arg.Any<Audio>());
        }

        [Fact]
        public void GetAudioEntriesRangeAsync_ReturnsOutOfRangeException()
        {
            var sut = new AudioServiceImplBuilder()
                .Build();

            Func<Task> action = async ()
                => await sut.GetAudioEntriesRangeAsync(0, 42, CancellationToken.None);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public async Task GetAudioEntriesRangeAsync_CallsAudioRepository()
        {
            var mapper = Substitute.For<IMapper>();
            var repository = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repository)
                .WithMapper(mapper)
                .Build();

            await sut.GetAudioEntriesRangeAsync(2, 1, CancellationToken.None);
            await repository.Received().GetRangeAsync(1, 1, CancellationToken.None);
        }

        [Fact]
        public async Task GetAudioEntriesRangeAsync_CallsAutoMapper()
        {
            var mapper = Substitute.For<IMapper>();
            var repository = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repository)
                .WithMapper(mapper)
                .Build();

            await sut.GetAudioEntriesRangeAsync(2, 1, CancellationToken.None);
            mapper.ReceivedWithAnyArgs();
        }

        [Fact]
        public async Task GetAudioEntriesRangeAsync_ReturnsNonNull()
        {
            var mapper = new MapperBuilder().WithDefaultProfile().Build();
            var repository = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repository)
                .WithMapper(mapper)
                .Build();

            var expectedValue = await sut.GetAudioEntriesRangeAsync(1, 1, CancellationToken.None);
            expectedValue.Should().NotBeNull();
        }
    }
}
