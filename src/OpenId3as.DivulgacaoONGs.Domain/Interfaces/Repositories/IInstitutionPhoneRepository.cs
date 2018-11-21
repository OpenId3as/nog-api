using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface IInstitutionPhoneRepository : IPostgresRepository<InstitutionPhone>, IDisposable
    {
    }
}
