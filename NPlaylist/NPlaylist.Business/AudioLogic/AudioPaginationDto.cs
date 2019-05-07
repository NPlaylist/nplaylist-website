using System.Collections.Generic;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist.Business.AudioLogic
{
    public class AudioPaginationDto
    {
        public List<Audio> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalNbOfPages { get; set; }
    }
}
