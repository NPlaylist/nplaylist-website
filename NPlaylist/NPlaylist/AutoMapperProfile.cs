using AutoMapper;
using NPlaylist.Models;
using NPlaylist.Persistence.DbModels;

namespace NPlaylist
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Audio, AudioEntryViewModel>().ReverseMap();
        }
    }
}
