using AutoMapper;
using FluentAssertions;
using NPlaylist.Persistence.AudioEntries;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Tests.Services.AudioService
{
    public class AudioServiceTests
    {
        [Fact]
        public async Task GetAudioEntriesRange_CallsAudioRepository()
        {
            var repository = Substitute.For<IAudioEntriesRepository>();
            var sut = new AudioServiceImplBuilder()
                .WithAudioRepository(repository)
                .Build();

            await sut.GetAudioEntriesAsync(CancellationToken.None);
            await repository.Received().GetAllAsync(CancellationToken.None);
        }

        [Fact]
        public async Task GetAudioEntriesRange_CallsAutoMapper()
        {
            var mapper = Substitute.For<IMapper>();
            var sut = new AudioServiceImplBuilder()
                .WithAutoMapper(mapper)
                .Build();

            await sut.GetAudioEntriesAsync(CancellationToken.None);
            mapper.ReceivedWithAnyArgs();
        }

        [Fact]
        public async Task GetAudioEntriesRange_ReturnsNonNull()
        {
            var mapper = new MapperBuilder().WithDefaultProfile().Build();
            
            var sut = new AudioServiceImplBuilder().WithAutoMapper(mapper)
                .Build();
            var expectedValue = await sut.GetAudioEntriesAsync(CancellationToken.None);
            expectedValue.Should().NotBeNull();
        }
    }
}