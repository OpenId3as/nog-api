using AutoMapper;

namespace OpenId3as.DivulgacaoONGs.Application.AutoMapper.Config
{
    public class AutoMapperConfig
    {
        public MapperConfiguration RegisterMappingsInit()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewToDomainModelMappingProfile());
                cfg.AddProfile(new DomainToViewModelMappingProfile());
            });

            config.CreateMapper();
            return config;
        }
    }
}
