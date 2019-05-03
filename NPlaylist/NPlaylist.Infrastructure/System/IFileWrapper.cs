namespace NPlaylist.Infrastructure.System
{
    public interface IFileWrapper
    {
        void Delete(string filePath);

        bool Exists(string filePath);
    }
}