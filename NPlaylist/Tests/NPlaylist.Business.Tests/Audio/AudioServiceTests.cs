using NPlaylist.Business.Audio;
using NPlaylist.Business.LocalRepository;
using NPlaylist.Persistence.AudioEntries;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Business.Tests.Audio
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

            await sut.UploadAsync(audio, CancellationToken.None);

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

            await sut.UploadAsync(audio, CancellationToken.None);

            audioRepoMock.Received().Add(Arg.Any<Persistence.DbModels.Audio>());
        }

        [Fact]
        public async Task UploadAsync_SaveAsyncToAudioEntriesRepositoryIsCalled_True()
        {
            var audioRepoMock = Substitute.For<IAudioEntriesRepository>();

            var sut = new AudioServiceBuilder()
                .WithAudioEntriesRepo(audioRepoMock)
                .Build();

            var audio = new AudioUploadDtoBuilder().Build();

            await sut.UploadAsync(audio, CancellationToken.None);

            await audioRepoMock.Received().SaveAsync(CancellationToken.None);
        }
    }
}