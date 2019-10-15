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
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            //Postgres
            CreateMap<Collaborator, CollaboratorViewModel>();
            CreateMap<CollaboratorAddress, CollaboratorAddressViewModel>();
            CreateMap<CollaboratorPhone, CollaboratorPhoneViewModel>();
            CreateMap<Institution, InstitutionViewModel>();
            CreateMap<InstitutionAddress, InstitutionAddressViewModel>();
            CreateMap<Institution, InstitutionPhoneViewModel>();
            CreateMap<Volunteer, VolunteerViewModel>();
            CreateMap<VolunteerAddress, VolunteerAddressViewModel>();
            CreateMap<VolunteerPhone, VolunteerPhoneViewModel>();
            CreateMap<User, UserViewModel>();

            //Mongo
            CreateMap<Page.BasePage, PageViewModels.BasePageViewModel>();
            CreateMap<Page.Collaborator, PageViewModels.CollaboratorViewModel>();
            CreateMap<Page.Contact, PageViewModels.ContactViewModel>();
            CreateMap<Page.DataPage, PageViewModels.DataPageViewModel>();
            CreateMap<Page.Home, PageViewModels.HomeViewModel>();
            CreateMap<Page.HowToHelp, PageViewModels.HowToHelpViewModel>();
            CreateMap<Page.Language, PageViewModels.LanguageViewModel>();
            CreateMap<Page.DataLabels, PageViewModels.DataLabelsViewModel>();
            CreateMap<Page.FooterLabels, PageViewModels.FooterLabelsViewModel>();
            CreateMap<Page.HeaderLabels, PageViewModels.HeaderLabelsViewModel>();
            CreateMap<Page.ValidationLabels, PageViewModels.ValidationLabelsViewModel>();
            CreateMap<Page.Logo, PageViewModels.LogoViewModel>();
            CreateMap<Page.Menu, PageViewModels.MenuViewModel>();
            CreateMap<Page.MenuItem, PageViewModels.MenuItemViewModel>();
            CreateMap<Page.MenuItemDetail, PageViewModels.MenuItemDetailViewModel>();
            CreateMap<Page.Style, PageViewModels.StyleViewModel>();
            CreateMap<Page.Volunteer, PageViewModels.VolunteerViewModel>();
            CreateMap<Page.WhoAreWe, PageViewModels.WhoAreWeViewModel>();
            CreateMap<Page.LanguagePage, PageViewModels.LanguagePageViewModel>();
        }
    }
}
