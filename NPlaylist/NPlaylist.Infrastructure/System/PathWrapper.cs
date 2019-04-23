using System.IO;

namespace NPlaylist.Infrastructure.System
{
    public class PathWrapper : IPathWrapper
    {
        public string GetDirectoryName(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        public string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }
    }
}