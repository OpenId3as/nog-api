using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page
{
    public interface IVolunteerRepository : IMongoRepository<Volunteer>, IDisposable
    {
    }
}
