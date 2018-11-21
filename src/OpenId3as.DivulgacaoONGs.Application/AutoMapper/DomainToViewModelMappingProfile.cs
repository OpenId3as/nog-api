using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;

namespace OpenId3as.DivulgacaoONGs.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Collaborator, CollaboratorViewModel>();
            CreateMap<CollaboratorAddress, CollaboratorAddressViewModel>();
            CreateMap<CollaboratorPhone, CollaboratorPhoneViewModel>();
            CreateMap<Institution, InstitutionViewModel>();
            CreateMap<InstitutionAddress, InstitutionAddressViewModel>();
            CreateMap<Institution, InstitutionPhoneViewModel>();
            CreateMap<Volunteer, VolunteerViewModel>();
            CreateMap<VolunteerAddress, VolunteerAddressViewModel>();
            CreateMap<VolunteerPhone, VolunteerPhoneViewModel>();
        }
    }
}
