using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using NPlaylist.Business.AudioLogic;
using NPlaylist.Business.LocalRepository;
using NPlaylist.Persistence.AudioEntries;
using NPlaylist.Persistence.AudioMetaEntries;
using NPlaylist.Persistence.DbModels;
using NSubstitute;
using Xunit;

namespace NPlaylist.Business.Tests.AudioLogic
{
    public class AudioServiceTests
    {
        [Fact]
        public async Task UploadAsync_AddAsyncToAudioLocalRepositoryIsCalled_True()
        {
            var audioLocalRepoMock = Substitute.For<IAudioLocalRepository>();
            var sut = new AudioServiceBuilder()
                .WithLocalRepo(audioLocalRepoMock)
                .Build();

            var audio = new AudioUploadDtoBuilder().Build();

            await sut.UploadAudioAsync(audio, CancellationToken.None);

            await audioLocalRepoMock.Received().AddAsync(Arg.Any<AudioUploadDto>(), CancellationToken.None);
        }

        [Fact]
        public async Task UploadAsync_AddToAudioEntriesRepositoryIsCalled_True()
        {
            var audioRepoMock = Substitute.For<IAudioEntriesRepository>();

            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(audioRepoMock)
                .Build();

            var audio = new AudioUploadDtoBuilder().Build();

            await sut.UploadAudioAsync(audio, CancellationToken.None);

            await audioRepoMock.Received().AddAsync(Arg.Any<Audio>(), CancellationToken.None);
        }

        [Fact]
        public async Task UploadAsync_SaveAsyncToAudioEntriesRepositoryIsCalled_True()
        {
            var audioRepoMock = Substitute.For<IAudioEntriesRepository>();

            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(audioRepoMock)
                .Build();

            var audio = new AudioUploadDtoBuilder().Build();

            await sut.UploadAudioAsync(audio, CancellationToken.None);

            await audioRepoMock.Received().SaveAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GetAudioAsync_ForNoAudios_ReturnsNull()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceBuilder().WithAudioEntriesRepo(repo).Build();

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

            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(repo)
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
            
            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(repo)
                .Build();

            Func<Task> actualAction = async () =>
                await sut.UpdateAudioAsync(new Audio(), CancellationToken.None);

            actualAction.Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateAudioAsync_ForExistingAudio_UpdatesAsExpected()
        {
            var repo = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(repo)
                .Build();

            await sut.UpdateAudioAsync(new Audio(), CancellationToken.None);

            repo.Received().Update(Arg.Any<Audio>());
        }

        [Fact]
        public void DeleteAudioAsync_FailedForDatabaseRemove_ThrowsExceptionWithMessage()
        {
            var audioRepo = Substitute.For<IAudioEntriesRepository>();
            var metaRepo = Substitute.For<IAudioMetaEntriesRepository>();
            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(audioRepo)
                .WithAudioMetaEntriesRepo(metaRepo)
                .Build();

            audioRepo.GetAudioIncludingMetaAsync(Guid.Empty, CancellationToken.None)
                .Returns(new Audio());
            audioRepo
                .When(x => x.Remove(Arg.Any<Audio>()))
                .Do(x => throw new Exception());

            Func<Task> actualAction = async () =>
                await sut.DeleteAudioAsync(Guid.Empty, CancellationToken.None);

            actualAction.Should().Throw<Exception>();
        }

        [Fact]
        public async Task DeleteAudioAsync_ForExistingAudio_DeletesAsExpected()
        {
            var audioRepo = Substitute.For<IAudioEntriesRepository>();
            audioRepo.GetById(Arg.Any<Guid>()).Returns(new Audio { Path = "Foo/Bar"});
            var metaRepo = Substitute.For<IAudioMetaEntriesRepository>();
            var localRepo = Substitute.For<IAudioLocalRepository>();
            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(audioRepo)
                .WithAudioMetaEntriesRepo(metaRepo)
                .WithLocalRepo(localRepo)
                .Build();

            audioRepo.GetAudioIncludingMetaAsync(Guid.Empty, CancellationToken.None)
                .Returns(new Audio());
            await sut.DeleteAudioAsync(Guid.Empty, CancellationToken.None);

            audioRepo.Received().Remove(Arg.Any<Audio>());
            audioRepo.Received().GetById(Arg.Any<Guid>());
            metaRepo.Received().Remove(Arg.Any<AudioMeta>());
        }

        [Fact]
        public void GetAudioPaginationAsync_ReturnsOutOfRangeException()
        {
            var sut = new AudioServiceBuilder()
                .Build();

            Func<Task> action = async ()
                => await sut.GetAudioPaginationAsync(0, 42, CancellationToken.None);
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public async Task GetAudioPaginationAsync_CallsAudioRepository()
        {
            var repository = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(repository)
                .Build();

            await sut.GetAudioPaginationAsync(2, 1, CancellationToken.None);
            await repository.Received().GetRangeAsync(1, 1, CancellationToken.None);
        }

        [Fact]
        public async Task GetAudioPaginationAsync_ReturnsNonNull()
        {
            var repository = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(repository)
                .Build();

            var expectedValue = await sut.GetAudioPaginationAsync(1, 1, CancellationToken.None);
            expectedValue.Should().NotBeNull();
        }
    }
}