using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface ILogoRepository : IMongoRepository<Logo>, IDisposable
    {
    }
}
