namespace NPlaylist.Wrappers.PathWrapper
{
    public interface IPathWrapper
    {
        string GetExtension(string fileName);
        string GetDirectoryName(string filePath);
    }
}
