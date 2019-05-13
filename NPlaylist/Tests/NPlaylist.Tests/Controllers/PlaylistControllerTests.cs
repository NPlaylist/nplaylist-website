using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NPlaylist.Tests.Controllers
{
    public class PlaylistControllerTests
    {
        [Fact]
        public async Task Index_ExpectedNotNull_ReturnsViewResult()
        {
            var controller = new PlaylistControllerBuilder()
                .Build();

            var result = await controller.Index(CancellationToken.None) as ViewResult;

            result.Should().NotBeNull();
        }

        [Fact]
        public void Index_ExpectedException_ReturnsOutOfRangeException()
        {
            var controller = new PlaylistControllerBuilder()
                .Build();

            Func<Task> result = async ()
                => await controller.Index(CancellationToken.None, 0);

            result.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}
