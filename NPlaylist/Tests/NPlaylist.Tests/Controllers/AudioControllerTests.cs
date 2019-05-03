using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NPlaylist.Services.AudioService;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Tests.Controllers
{
    public class AudioControllerTests
    {
        [Fact]
        public async Task Index_ExpectedNotNull_ReturnsViewResult()
        {
            var service = Substitute.For<IAudioService>();
            var controller = new AudioControllerBuilder().WithAudioService(service)
                .Build();

            var result = await controller.Index(CancellationToken.None) as ViewResult;

            result.Should().NotBeNull();
        }

        [Fact]
        public void Index_ExpectedException_ReturnsOutOfRangeException()
        {
            var service = Substitute.For<IAudioService>();
            var controller = new AudioControllerBuilder().WithAudioService(service)
                .Build();

            Func<Task> result = async ()
                => await controller.Index(CancellationToken.None, 0);

            result.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}