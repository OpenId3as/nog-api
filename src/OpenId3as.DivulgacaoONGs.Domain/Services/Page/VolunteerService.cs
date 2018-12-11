using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _volunteerRepository;
        public VolunteerService(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        public void Add(Volunteer volunteer)
        {
            _volunteerRepository.Add(volunteer);
        }

        public void AddRange(IEnumerable<Volunteer> volunteer)
        {
            _volunteerRepository.AddRange(volunteer);
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

        public void Update(Volunteer volunteer, long id)
        {
            _volunteerRepository.Update(volunteer, id);
        }
    }
}
