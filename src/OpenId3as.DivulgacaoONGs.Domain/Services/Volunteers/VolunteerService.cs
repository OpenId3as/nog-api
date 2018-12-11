using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Volunteers
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public VolunteerService(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        public Volunteer Add(Volunteer volunteer)
        {
            return _volunteerRepository.Add(volunteer);
        }

        public void Delete(long id)
        {
            _volunteerRepository.Delete(id);
        }

        public void Dispose()
        {
            _volunteerRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Volunteer> GetAll()
        {
            return _volunteerRepository.GetAll();
        }

        public Volunteer GetById(long id)
        {
            return _volunteerRepository.GetById(id);
        }

        public Volunteer Update(Volunteer volunteer)
        {
            return _volunteerRepository.Update(volunteer);
        }
    }
}
