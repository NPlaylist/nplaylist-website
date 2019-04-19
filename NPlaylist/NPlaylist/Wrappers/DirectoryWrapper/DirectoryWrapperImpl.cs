using System.IO;

namespace NPlaylist.Wrappers.DirectoryWrapper
{
    public class DirectoryWrapperImpl : IDirectoryWrapper
    {
        public void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}
