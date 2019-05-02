using System.IO;

namespace NPlaylist.Business
{
    public interface IFileStreamFactory
    {
        Stream Create(string path, FileMode fileMode, FileAccess fileAccess);
    }
}