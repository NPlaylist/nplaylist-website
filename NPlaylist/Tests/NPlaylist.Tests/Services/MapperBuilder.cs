using System.Collections.Generic;
using AutoMapper;

namespace NPlaylist.Tests.Services
{
    public class MapperBuilder
    {
        private readonly List<Profile> _profiles;

        public MapperBuilder()
        {
            _profiles = new List<Profile>();
        }

        public IMapper Build()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                foreach (var profile in _profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            return new Mapper(mapConfig);
        }

        public MapperBuilder WithProfiles(params Profile[] profiles)
        {
            _profiles.AddRange(profiles);
            return this;
        }

        public MapperBuilder WithDefaultProfile()
        {
            _profiles.Add(new AutoMapperProfile());
            return this;
        }
    }
}
