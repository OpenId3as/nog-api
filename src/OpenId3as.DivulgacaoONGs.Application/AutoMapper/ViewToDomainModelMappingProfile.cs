using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.ViewModels;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Entities;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using Page = OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using PageViewModels = OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;


namespace OpenId3as.DivulgacaoONGs.Application.AutoMapper
{
    public class ViewToDomainModelMappingProfile : Profile
    {
        public ViewToDomainModelMappingProfile()
        {
            //Postgres
            CreateMap<CollaboratorViewModel, Collaborator>();
            CreateMap<CollaboratorAddressViewModel, CollaboratorAddress>();
            CreateMap<CollaboratorPhoneViewModel, CollaboratorPhone>();
            CreateMap<InstitutionViewModel, Institution>();
            CreateMap<InstitutionAddressViewModel, InstitutionAddress>();
            CreateMap<InstitutionPhoneViewModel, Institution>();
            CreateMap<VolunteerViewModel, Volunteer>();
            CreateMap<VolunteerAddressViewModel, VolunteerAddress>();
            CreateMap<VolunteerPhoneViewModel, VolunteerPhone>();
            CreateMap<User, UserViewModel>();

            //Mongo
            CreateMap<PageViewModels.BasePageViewModel, Page.BasePage>();
            CreateMap<PageViewModels.CollaboratorViewModel, Page.Collaborator>();
            CreateMap<PageViewModels.ContactViewModel, Page.Contact>();
            CreateMap<PageViewModels.DataPageViewModel, Page.DataPage>();
            CreateMap<PageViewModels.HomeViewModel, Page.Home>();
            CreateMap<PageViewModels.HowToHelpViewModel, Page.HowToHelp>();
            CreateMap<PageViewModels.LanguageViewModel, Page.Language>();
            CreateMap<PageViewModels.DataLabelsViewModel, Page.DataLabels>();
            CreateMap<PageViewModels.FooterLabelsViewModel, Page.FooterLabels>();
            CreateMap<PageViewModels.HeaderLabelsViewModel, Page.HeaderLabels>();
            CreateMap<PageViewModels.ValidationLabelsViewModel, Page.ValidationLabels>();
            CreateMap<PageViewModels.LogoViewModel, Page.Logo>();
            CreateMap<PageViewModels.MenuViewModel, Page.Menu>();
            CreateMap<PageViewModels.MenuItemViewModel, Page.MenuItem>();
            CreateMap<PageViewModels.MenuItemDetailViewModel, Page.MenuItemDetail>();
            CreateMap<PageViewModels.StyleViewModel, Page.Style>();
            CreateMap<PageViewModels.VolunteerViewModel, Page.Volunteer>();
            CreateMap<PageViewModels.WhoAreWeViewModel, Page.WhoAreWe>();
            CreateMap<PageViewModels.LanguagePageViewModel, Page.LanguagePage>();
        }
    }
}
