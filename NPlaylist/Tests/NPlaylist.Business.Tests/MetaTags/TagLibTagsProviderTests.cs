using FluentAssertions;
using NPlaylist.Business.MetaTags;
using NPlaylist.Persistence.DbModels;
using Xunit;

namespace NPlaylist.Business.Tests.MetaTags
{
    public class TagLibTagsProviderTests
    {
        [Fact]
        public void GetTags_ReturnsAudioMetaWithCorrectAlbum_True()
        {
            var tagLibWrapperMock = new TagWrapperMockBuilder().TagWithAlbum("Foo Album").Build();

            var sut = new TagLibTagsProvider(tagLibWrapperMock);

            sut.GetTags("test").Album.Should().Be("Foo Album");
        }

        [Fact]
        public void GetTags_ReturnsAudioMetaWithCorrectTitle_True()
        {
            var tagLibWrapperMock = new TagWrapperMockBuilder().TagWithTitle("Foo Title").Build();
            var sut = new TagLibTagsProvider(tagLibWrapperMock);

            sut.GetTags("test").Title.Should().Be("Foo Title");
        }

        [Fact]
        public void GetTags_ReturnsAudioMetaObject_True()
        {
            var tagLibWrapperMock = new TagWrapperMockBuilder().Build();
            var sut = new TagLibTagsProvider(tagLibWrapperMock);

            sut.GetTags("test").Should().BeOfType(typeof(AudioMeta));
        }
    }
}