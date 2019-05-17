using AutoMapper;
using NPlaylist.Business.AudioLogic;
using NPlaylist.Business.PlaylistLogic;
using NPlaylist.Persistence.DbModels;
using NPlaylist.ViewModels.Audio;
using NPlaylist.ViewModels.Playlist;

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

            CreateMap<Playlist, PlaylistIndexViewModel>()
                .ForMember(vm => vm.ShortDescription, m => m.MapFrom(
                    u => (u.Description.Length > 100)
                        ? u.Description.Substring(0, 100) + "..."
                        : u.Description))
                .ForMember(vm => vm.EntriesCount, m => m.MapFrom(
                    u => u.AudioPlaylists.Count));

            CreateMap<AudioMeta, AudioMetaViewModel>().ReverseMap();
            CreateMap<Audio, AudioViewModel>().ReverseMap();
            CreateMap<AudioPaginationDto, AudioPaginatedListViewModel>();

            CreateMap<Playlist, PlaylistCreateDto>().ReverseMap();
            CreateMap<PlaylistPaginationDto, PlaylistPaginatedListViewModel>();

            CreateMap<Playlist, PlaylistDetailsDto>();
            CreateMap<PlaylistDetailsDto, PlaylistDetailsViewModel>();
        }
    }
}
