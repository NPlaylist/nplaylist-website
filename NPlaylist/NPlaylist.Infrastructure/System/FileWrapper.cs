using System.IO;

namespace NPlaylist.Infrastructure.System
{
    public class FileWrapper : IFileWrapper
    {
        public void Delete(string filePath)
        {
            File.Delete(filePath);
        }

        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}