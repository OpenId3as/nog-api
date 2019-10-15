using AutoMapper;

namespace OpenId3as.DivulgacaoONGs.Application.AutoMapper.Config
{
    public class AutoMapperConfig
    {
        public static void RegisterMappingsInit()
        {
            //var config = new MapperConfiguration(cfg => {
            //    cfg.AddProfile(new ViewToDomainModelMappingProfile());
            //    cfg.AddProfile(new DomainToViewModelMappingProfile());
            //});
            //config.CreateMapper();
            //return config;
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new ViewToDomainModelMappingProfile());
                cfg.AddProfile(new DomainToViewModelMappingProfile());
            });
        }
    }
}
