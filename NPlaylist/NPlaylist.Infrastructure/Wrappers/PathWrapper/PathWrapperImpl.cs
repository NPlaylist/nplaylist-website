using System.IO;

namespace NPlaylist.Infrastructure.Wrappers.PathWrapper
{
    public class PathWrapperImpl : IPathWrapper
    {
        public string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public string GetDirectoryName(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }
    }
}
