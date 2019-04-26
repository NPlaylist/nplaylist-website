namespace NPlaylist.Business.Providers
{
    public interface IPathProvider
    {
        string BuildPath(string fileName);
    }
}