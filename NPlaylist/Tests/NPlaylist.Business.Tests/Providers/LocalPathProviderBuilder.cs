using System;
using System.IO;
using NPlaylist.Business.Providers;
using NPlaylist.Infrastructure.System;
using NSubstitute;

namespace NPlaylist.Business.Tests.Providers
{
    internal class LocalPathProviderBuilder
    {
        private readonly IGuidWrapper _guidMock;
        private readonly IPathWrapper _pathMock;
        private string _webRootPath;

        public LocalPathProviderBuilder()
        {
            _pathMock = Substitute.For<IPathWrapper>();
            _guidMock = Substitute.For<IGuidWrapper>();
            _webRootPath = "testWebRoot";
        }

        public LocalPathProvider Build()
        {
            return new LocalPathProvider(_webRootPath, _pathMock, _guidMock);
        }

        public LocalPathProviderBuilder WithGetExtension(string fileName)
        {
            _pathMock.GetExtension("test").Returns(Path.GetExtension(fileName));
            return this;
        }

        public LocalPathProviderBuilder WithGuid(Guid guid)
        {
            _guidMock.NewGuid().Returns(guid);
            return this;
        }

        public LocalPathProviderBuilder WithPathCombine(params string[] pathParts)
        {
            _pathMock.Combine(Arg.Any<string[]>()).Returns(Path.Combine(pathParts));
            return this;
        }

        public LocalPathProviderBuilder WithWebRootPath(string path)
        {
            _webRootPath = path;
            return this;
        }
    }
}