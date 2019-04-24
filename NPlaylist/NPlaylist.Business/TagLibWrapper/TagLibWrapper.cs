using TagLib;

namespace NPlaylist.Business.TagLibWrapper
{
    public class TagLibWrapper : ITagLibWrapper
    {
        public File Create(string filePath)
        {
            return File.Create(filePath);
        }
    }
}