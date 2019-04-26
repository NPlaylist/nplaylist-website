using FluentAssertions;
using Xunit;

namespace NPlaylist.Business.Tests.Providers
{
    public class PathProviderTests
    {
        [Fact]
        public void BuildPath_PathProviderShouldReturnString_True()
        {
            var sut = new LocalPathProviderBuilder()
                .Build();

            sut.BuildPath("Test").Should().BeOfType(typeof(string));
        }

        [Fact]
        public void BuildPath_PathShouldContainExtension_True()
        {
            var fileName = "test.mp3";
            var sut = new LocalPathProviderBuilder()
                .WithPathCombine("v", fileName)
                .Build();

            sut.BuildPath("test.mp3").Should().Contain(".mp3");
        }

        [Fact]
        public void BuildPath_PathShouldNotBeNullOrEmptyIfDataProvided_True()
        {
            var fileName = "test.mp3";
            var sut = new LocalPathProviderBuilder()
                .WithPathCombine(fileName)
                .Build();

            sut.BuildPath("test.mp3").Should().NotBeNullOrEmpty();
        }
    }
}