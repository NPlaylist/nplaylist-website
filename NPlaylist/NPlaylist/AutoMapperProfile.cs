using AutoMapper;
using NPlaylist.Models;
using NPlaylist.Persistence.DbModels;
using NPlaylist.ViewModels;

namespace NPlaylist
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Audio, AudioEntryViewModel>()
                .ForMember(vm => vm.FullName, m => m.MapFrom(
                    u => (u.Meta != null)
                        ? u.Meta.Author + " - " + u.Meta.Title
                        : "Unknown"));
        }
    }
}
