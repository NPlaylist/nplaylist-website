using TagLib;

namespace NPlaylist.Business.TagLib
{
    public interface ITagLibWrapper
    {
        File Create(string filePath);
    }
}