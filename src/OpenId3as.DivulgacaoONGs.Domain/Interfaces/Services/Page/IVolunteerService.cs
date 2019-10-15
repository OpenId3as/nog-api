using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IVolunteerService : IDisposable
    {
        void Add(Volunteer volunteer);
        void AddRange(IEnumerable<Volunteer> volunteer);
        void Update(Volunteer volunteer, long id);
        void Delete(long id);
        Volunteer GetById(long id);
        IEnumerable<Volunteer> GetAll();
        Volunteer GetInstitutionByLanguage(string language, string institution);
    }
}
