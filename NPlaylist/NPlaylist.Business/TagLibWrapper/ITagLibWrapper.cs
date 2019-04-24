using TagLib;

namespace NPlaylist.Business.TagLibWrapper
{
    public interface ITagLibWrapper
    {
        File Create(string filePath);
    }
}