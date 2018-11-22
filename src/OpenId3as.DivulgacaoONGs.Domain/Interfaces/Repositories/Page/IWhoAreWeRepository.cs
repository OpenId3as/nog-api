using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page
{
    public interface IWhoAreWeRepository : IMongoRepository<WhoAreWe>, IDisposable
    {
    }
}
