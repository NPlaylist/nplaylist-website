using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using NPlaylist.Models.Audio;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.DbModels;
using NSubstitute;
using Xunit;

namespace NPlaylist.Tests.Services.AudioService
{
    public class AudioServiceImplTests
    {
        [Fact]
        public void GetAudioAsync_ForNoAudios_ThrowsException()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceImplBuilder().WithAudioRepo(repo).Build();

            Func<Task> actualAction = async () => await sut.GetAudioAsync(Guid.Empty, CancellationToken.None);
            actualAction.Should().Throw<KeyNotFoundException>();
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
                await sut.UpdateAudioAsync(new AudioViewModel(), CancellationToken.None);

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

            await sut.UpdateAudioAsync(new AudioViewModel(), CancellationToken.None);

            repo.Received().Update(Arg.Any<Audio>());
        }

        [Fact]
        public async Task GetAudioEntriesRange_CallsAudioRepository()
        {
            var repository = Substitute.For<IAudioEntriesRepository>();
            var mapper = new MapperBuilder().WithDefaultProfile().Build();
            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repository)
                .WithMapper(mapper)
                .Build();

            await sut.GetAudioEntriesAsync(CancellationToken.None);
            await repository.Received().GetAllAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GetAudioEntriesRange_CallsAutoMapper()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            var mapper = Substitute.For<IMapper>();
            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repo)
                .WithMapper(mapper)
                .Build();

            await sut.GetAudioEntriesAsync(CancellationToken.None);
            mapper.ReceivedWithAnyArgs();
        }

        [Fact]
        public async Task GetAudioEntriesRange_ReturnsNonNull()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            var mapper = new MapperBuilder().WithDefaultProfile().Build();

            var sut = new AudioServiceImplBuilder()
                .WithAudioRepo(repo)
                .WithMapper(mapper)
                .Build();
            var expectedValue = await sut.GetAudioEntriesAsync(CancellationToken.None);
            expectedValue.Should().NotBeNull();
        }
    }
}
