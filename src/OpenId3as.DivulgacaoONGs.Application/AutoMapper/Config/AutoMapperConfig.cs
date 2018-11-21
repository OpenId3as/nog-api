using AutoMapper;

namespace OpenId3as.DivulgacaoONGs.Application.AutoMapper.Config
{
    public class AutoMapperConfig
    {
        public static void RegisterMappingsInit()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ViewToDomainModelMappingProfile());
                cfg.AddProfile(new DomainToViewModelMappingProfile());
            });
        }
    }
}
