using System.IO;

namespace NPlaylist.Infrastructure.Wrappers.DirectoryWrapper
{
    public class DirectoryWrapperImpl : IDirectoryWrapper
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}