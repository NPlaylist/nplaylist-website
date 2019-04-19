using Microsoft.AspNetCore.Hosting;
using NPlaylist.Wrappers.PathWrapper;
using System;

namespace NPlaylist.Managers.PathProvider
{
    public class StandardAudioPathProvider : IPathProvider
    {
        private readonly string _appEnvironmentPath;
        private readonly IPathWrapper _pathWrapper;

        public StandardAudioPathProvider(IHostingEnvironment appEnvironment, IPathWrapper pathWrapper)
        {
            _pathWrapper = pathWrapper;
            _appEnvironmentPath = appEnvironment.WebRootPath;
        }

        public string BuildPath(string fileName)
        {
            var fileExtension = _pathWrapper.GetExtension(fileName).Replace(".", "");
            var path = $"{_appEnvironmentPath}/Files/{fileExtension}/{Guid.NewGuid().ToString()}{fileName}";

            return path;
        }
    }
}
