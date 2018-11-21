using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services
{
    public interface IVolunteerService : IDisposable
    {
        Volunteer Add(Volunteer volunteer);
        Volunteer Update(Volunteer volunteer);
        Volunteer GetById(Int64 id);
        IEnumerable<Volunteer> GetAll();
        void Delete(Int64 id);
    }
}
