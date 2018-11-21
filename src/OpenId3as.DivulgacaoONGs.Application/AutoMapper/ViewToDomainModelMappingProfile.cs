using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;

namespace OpenId3as.DivulgacaoONGs.Application.AutoMapper
{
    public class ViewToDomainModelMappingProfile : Profile
    {
        public ViewToDomainModelMappingProfile()
        {
            CreateMap<CollaboratorViewModel, Collaborator>();
            CreateMap<CollaboratorAddressViewModel, CollaboratorAddress>();
            CreateMap<CollaboratorPhoneViewModel, CollaboratorPhone>();
            CreateMap<InstitutionViewModel, Institution>();
            CreateMap<InstitutionAddressViewModel, InstitutionAddress>();
            CreateMap<InstitutionPhoneViewModel, Institution>();
            CreateMap<VolunteerViewModel, Volunteer>();
            CreateMap<VolunteerAddressViewModel, VolunteerAddress>();
            CreateMap<VolunteerPhoneViewModel, VolunteerPhone>();
        }
    }
}
