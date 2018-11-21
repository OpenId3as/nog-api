using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface IInstitutionRepository : IPostgresRepository<Institution>, IDisposable
    {
    }
}
