using NPlaylist.Controllers;

namespace NPlaylist.Tests.Controllers
{
    public class PlaylistControllerBuilder
    {
        public PlaylistControllerBuilder()
        {
        }

        public PlaylistController Build()
        {
            return new PlaylistController();
        }
    }
}
