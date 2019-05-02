using Microsoft.AspNetCore.Http;
using NPlaylist.Infrastructure.System;
using NSubstitute;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Business.Tests.Audio
{
    public class AudioLocalRepositoryTests
    {
        [Fact]
        public async Task AddAsync_FileCopyToAsyncShouldBeCalled_True()
        {
            var stream = Substitute.For<IFileStreamFactory>();
            stream.Create(Arg.Any<string>(), Arg.Any<FileMode>(), Arg.Any<FileAccess>()).Returns(new MemoryStream());
            var sut = new AudioLocalRepositoryBuilder()
                .WithFileStreamFactory(stream)
                .Build();

            var fileMock = Substitute.For<IFormFile>();

            var audio = new AudioUploadDtoBuilder()
                .WithFile(fileMock)
                .WithPath("test")
                .Build();

            await sut.AddAsync(audio, CancellationToken.None);
            await fileMock.Received().CopyToAsync(Arg.Any<MemoryStream>(), CancellationToken.None);
        }

        [Fact]
        public async Task AddAsync_GetDirectoryShouldBeCalled_True()
        {
            var pathMock = Substitute.For<IPathWrapper>();
            pathMock.GetDirectoryName("test").Returns("test");
            var stream = Substitute.For<IFileStreamFactory>();
            stream.Create(Arg.Any<string>(), Arg.Any<FileMode>(), Arg.Any<FileAccess>()).Returns(new MemoryStream());
            var sut = new AudioLocalRepositoryBuilder()
                .WithPathWrapper(pathMock)
                .WithFileStreamFactory(stream)
                .Build();

            var audio = new AudioUploadDtoBuilder()
                .WithPath("test")
                .Build();

            await sut.AddAsync(audio, CancellationToken.None);
            pathMock.Received().GetDirectoryName("test");
        }

        [Fact]
        public async Task AddAsync_StreamCreateCalledWithCorrectParameters_True()
        {
            var stream = Substitute.For<IFileStreamFactory>();
            stream.Create(Arg.Any<string>(), Arg.Any<FileMode>(), Arg.Any<FileAccess>()).Returns(new MemoryStream());
            var sut = new AudioLocalRepositoryBuilder()
                .WithFileStreamFactory(stream)
                .Build();

            var audio = new AudioUploadDtoBuilder()
                .WithPath("test")
                .Build();

            await sut.AddAsync(audio, CancellationToken.None);
            stream.Received().Create("test", FileMode.Create, FileAccess.Write);
        }
    }
}