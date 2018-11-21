using Microsoft.Extensions.DependencyInjection;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.Services.Collaborators;
using OpenId3as.DivulgacaoONGs.Application.Services.Institutions;
using OpenId3as.DivulgacaoONGs.Application.Services.Page;
using OpenId3as.DivulgacaoONGs.Application.Services.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Domain.Services.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Services.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Services.Page;
using OpenId3as.DivulgacaoONGs.Domain.Services.Volunteers;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Collaborators;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Institutions;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Volunteers;
using OpenId3as.DivulgacaoONGs.Infra.Data.UoW;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //APPLICATION
            services.AddScoped<ICollaboratorAppService, CollaboratorAppService>();
            services.AddScoped<IInstitutionAppService, InstitutionAppService>();
            services.AddScoped<IVolunteerAppService, VolunteerAppService>();

            services.AddScoped<ILogoAppService, LogoAppService>();

            //DOMAIN
            services.AddScoped<ICollaboratorService, CollaboratorService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IVolunteerService, VolunteerService>();

            services.AddScoped<ILogoService, LogoService>();

            //INFRA DATA
            services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();

            services.AddScoped<ILogoRepository, LogoRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<VetorContext>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped(typeof(IPostgresRepository<>), typeof(PostgresRepository<>));
        }
    }
}
