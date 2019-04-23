using FluentAssertions;
using NPlaylist.Controllers;
using Xunit;

namespace NPlaylist.Tests
{
    public class AudioControllerTests
    {
        [Fact]
        public void IndexReturnsViewResult_ExpectedNotNull()
        {
            var controller = new AudioController();
            
            var result = controller.Index();
            result.Should().NotBeNull();
        }
    }
}
