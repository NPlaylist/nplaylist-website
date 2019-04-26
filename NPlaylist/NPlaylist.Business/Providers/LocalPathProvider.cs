using NPlaylist.Infrastructure.System;

namespace NPlaylist.Business.Providers
{
    public class LocalPathProvider : IPathProvider
    {
        private readonly string _appEnvironmentPath;
        private readonly IGuidWrapper _guidWrapper;
        private readonly IPathWrapper _pathWrapper;

        public LocalPathProvider(
            string appEnvironmentPath,
            IPathWrapper pathWrapper,
            IGuidWrapper guidWrapper)
        {
            _guidWrapper = guidWrapper;
            _pathWrapper = pathWrapper;
            _appEnvironmentPath = appEnvironmentPath;
        }

        public string BuildPath(string fileName)
        {
            var fileExtension = _pathWrapper.GetExtension(fileName).Replace(".", "");
            var fullPath = _pathWrapper.Combine(_appEnvironmentPath, "Files", fileExtension, _guidWrapper.NewGuid() + fileName);

            return fullPath;
        }
    }
}