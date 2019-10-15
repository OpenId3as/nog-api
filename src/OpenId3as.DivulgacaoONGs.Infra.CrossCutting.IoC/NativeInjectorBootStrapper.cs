using Microsoft.Extensions.DependencyInjection;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.Services;
using OpenId3as.DivulgacaoONGs.Application.Services.Collaborators;
using OpenId3as.DivulgacaoONGs.Application.Services.Institutions;
using OpenId3as.DivulgacaoONGs.Application.Services.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Domain.Services;
using OpenId3as.DivulgacaoONGs.Domain.Services.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Services.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Services.Volunteers;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Security;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Collaborators;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Institutions;
using OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Volunteers;
using OpenId3as.DivulgacaoONGs.Infra.Data.UoW;
using ILog = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using IPageApp = OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using IPageRep = OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using IPageSvc = OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using LogRep = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Repositories;
using PageApp = OpenId3as.DivulgacaoONGs.Application.Services.Page;
using PageRep = OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page;
using PageSvc = OpenId3as.DivulgacaoONGs.Domain.Services.Page;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //APPLICATION
            services.AddScoped<ICollaboratorAppService, CollaboratorAppService>();
            services.AddScoped<IInstitutionAppService, InstitutionAppService>();
            services.AddScoped<ILoginAppService, LoginAppService>();
            services.AddScoped<IVolunteerAppService, VolunteerAppService>();
            services.AddScoped<IUserAppService, UserAppService>();

            services.AddScoped<IPageApp.ICollaboratorAppService, PageApp.CollaboratorAppService>();
            services.AddScoped<IPageApp.IContactAppService, PageApp.ContactAppService>();
            services.AddScoped<IPageApp.IHomeAppService, PageApp.HomeAppService>();
            services.AddScoped<IPageApp.IHowToHelpAppService, PageApp.HowToHelpAppService>();
            services.AddScoped<IPageApp.ILanguageAppService, PageApp.LanguageAppService>();
            services.AddScoped<IPageApp.ILogoAppService, PageApp.LogoAppService>();
            services.AddScoped<IPageApp.IMenuAppService, PageApp.MenuAppService>();
            services.AddScoped<IPageApp.IVolunteerAppService, PageApp.VolunteerAppService>();
            services.AddScoped<IPageApp.IWhoAreWeAppService, PageApp.WhoAreWeAppService>();

            //DOMAIN
            services.AddScoped<ICollaboratorService, CollaboratorService>();
            services.AddScoped<IInstitutionService, InstitutionService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPageSvc.ICollaboratorService, PageSvc.CollaboratorService>();
            services.AddScoped<IPageSvc.IContactService, PageSvc.ContactService>();
            services.AddScoped<IPageSvc.IHomeService, PageSvc.HomeService>();
            services.AddScoped<IPageSvc.IHowToHelpService, PageSvc.HowToHelpService>();
            services.AddScoped<IPageSvc.ILanguageService, PageSvc.LanguageService>();
            services.AddScoped<IPageSvc.ILogoService, PageSvc.LogoService>();
            services.AddScoped<IPageSvc.IMenuService, PageSvc.MenuService>();
            services.AddScoped<IPageSvc.IVolunteerService, PageSvc.VolunteerService>();
            services.AddScoped<IPageSvc.IWhoAreWeService, PageSvc.WhoAreWeService>();

            //INFRA DATA
            services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
            services.AddScoped<IInstitutionRepository, InstitutionRepository>();
            services.AddScoped<IVolunteerRepository, VolunteerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IPageRep.ICollaboratorRepository, PageRep.CollaboratorRepository>();
            services.AddScoped<IPageRep.IContactRepository, PageRep.ContactRepository>();
            services.AddScoped<IPageRep.IHomeRepository, PageRep.HomeRepository>();
            services.AddScoped<IPageRep.IHowToHelpRepository, PageRep.HowToHelpRepository>();
            services.AddScoped<IPageRep.ILanguageRepository, PageRep.LanguageRepository>();
            services.AddScoped<IPageRep.ILogoRepository, PageRep.LogoRepository>();
            services.AddScoped<IPageRep.IMenuRepository, PageRep.MenuRepository>();
            services.AddScoped<IPageRep.IVolunteerRepository, PageRep.VolunteerRepository>();
            services.AddScoped<IPageRep.IWhoAreWeRepository, PageRep.WhoAreWeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddScoped(typeof(IPostgresRepository<>), typeof(PostgresRepository<>));

            //LOG
            services.AddScoped(typeof(ILog.IPostgresRepository<>), typeof(LogRep.PostgresRepository<>));
            services.AddScoped<ILog.ILogRepository, LogRep.LogRepository>();

            //SECURITY
            services.AddScoped<IToken, Token>();
        }
    }
}
