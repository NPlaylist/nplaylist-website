using AutoMapper;
using NPlaylist.Business.AudioLogic;
using NPlaylist.Models;
using NPlaylist.Persistence.DbModels;
using NPlaylist.ViewModels;
using NPlaylist.ViewModels.Audio;

namespace NPlaylist
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Audio, AudioIndexViewModel>()
                .ForMember(vm => vm.FullName, m => m.MapFrom(
                    u => (u.Meta != null)
                        ? u.Meta.Author + " - " + u.Meta.Title
                        : "Unknown"));
            
            CreateMap<AudioMeta, AudioMetaViewModel>().ReverseMap();
            CreateMap<Audio, AudioViewModel>().ReverseMap();
            CreateMap<AudioPaginationDto, AudioPaginatedListViewModel>();
        }
    }
}
