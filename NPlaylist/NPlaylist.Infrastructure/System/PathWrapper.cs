using System.IO;
using NotImplementedException = System.NotImplementedException;

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

        public string Combine(params string[] pathParts)
        {
            return Path.Combine(pathParts);
        }
    }
}