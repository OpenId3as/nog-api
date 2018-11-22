using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IVolunteerService : IDisposable
    {
        void Add(Volunteer volunteer);
        void AddRange(IEnumerable<Volunteer> volunteer);
        void Update(Volunteer volunteer, Int64 id);
        void Delete(Int64 id);
        Volunteer GetById(Int64 id);
        IEnumerable<Volunteer> GetAll();
    }
}
