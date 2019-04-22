using TagLib;

namespace NPlaylist.Business.TagLib
{
    public class TagLibWrapper : ITagLibWrapper
    {
        public File Create(string filePath)
        {
            return File.Create(filePath);
        }
    }
}