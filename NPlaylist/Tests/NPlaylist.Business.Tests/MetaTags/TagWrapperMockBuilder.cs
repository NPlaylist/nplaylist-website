using NPlaylist.Business.TagLibWrapper;
using NSubstitute;
using TagLib;

namespace NPlaylist.Business.Tests.MetaTags
{
    internal class TagWrapperMockBuilder
    {
        private readonly File _fileMock;
        private readonly ITagLibWrapper _tagLibWrapperMock;
        private readonly Tag _tagMock;

        public TagWrapperMockBuilder()
        {
            _tagLibWrapperMock = Substitute.For<ITagLibWrapper>();
            _fileMock = Substitute.For<File>("Foo");
            _tagMock = Substitute.For<TagLib.Tag>();
        }

        public ITagLibWrapper Build()
        {
            _fileMock.Tag.Returns(_tagMock);
            _tagLibWrapperMock.Create("test").Returns(_fileMock);
            return _tagLibWrapperMock;
        }

        public TagWrapperMockBuilder TagWithAlbum(string album)
        {
            _tagMock.Album.Returns(album);
            return this;
        }

        public TagWrapperMockBuilder TagWithTitle(string title)
        {
            _tagMock.Title.Returns(title);
            return this;
        }
    }
}