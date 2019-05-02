using System.IO;

namespace NPlaylist.Business
{
    public class FileStreamImpl : IFileStreamFactory
    {
        public Stream Create(string path, FileMode fileMode, FileAccess fileAccess)
        {
            return new FileStream(path,fileMode,fileAccess);
        }
    }
}