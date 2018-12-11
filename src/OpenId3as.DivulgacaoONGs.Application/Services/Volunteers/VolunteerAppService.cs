using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Services.Volunteers
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

            _uow.BeginTransaction();
            var volunteerReturn = _volunteerService.Add(volunteer);
            _uow.Commit();

            volunteerViewModel = _mapper.Map<Volunteer, VolunteerViewModel>(volunteerReturn);
            return volunteerViewModel;
        }

        public void Delete(long id)
        {
            _uow.BeginTransaction();
            _volunteerService.Delete(id);
            _uow.Commit();
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

        public VolunteerViewModel Update(VolunteerViewModel volunteerViewModel)
        {
            _uow.BeginTransaction();
            _volunteerService.Update(_mapper.Map<VolunteerViewModel, Volunteer>(volunteerViewModel));
            _uow.Commit();
            return volunteerViewModel;
        }
    }
}
