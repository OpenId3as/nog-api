using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Services.Page
{
    public class VolunteerAppService : IVolunteerAppService
    {
        private readonly IMapper _mapper;
        private readonly IVolunteerService _volunteerService;
        private readonly IUnitOfWork _uow;

        public VolunteerAppService(IMapper mapper, IVolunteerService volunteerService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _volunteerService = volunteerService;
            _uow = uow;
        }

        public VolunteerViewModel Add(VolunteerViewModel volunteerViewModel)
        {
            var volunteer = _mapper.Map<VolunteerViewModel, Volunteer>(volunteerViewModel);
            _volunteerService.Add(volunteer);
            return volunteerViewModel;
        }

        public void Delete(long id)
        {
            _volunteerService.Delete(id);
        }

        public void Dispose()
        {
            _volunteerService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<VolunteerViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Volunteer>, IEnumerable<VolunteerViewModel>>(_volunteerService.GetAll());
        }

        public VolunteerViewModel GetById(long id)
        {
            return _mapper.Map<Volunteer, VolunteerViewModel>(_volunteerService.GetById(id));
        }

        public VolunteerViewModel GetInstitutionByLanguage(string language, string institution)
        {
            return _mapper.Map<Volunteer, VolunteerViewModel>(_volunteerService.GetInstitutionByLanguage(language, institution));
        }


        public VolunteerViewModel Update(VolunteerViewModel volunteerViewModel)
        {
            var volunteer = _mapper.Map<VolunteerViewModel, Volunteer>(volunteerViewModel);
            _volunteerService.Update(volunteer, volunteer.Id);
            return volunteerViewModel;
        }
    }
}
