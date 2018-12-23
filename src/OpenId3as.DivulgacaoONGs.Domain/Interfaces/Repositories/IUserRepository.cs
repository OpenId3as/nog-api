using OpenId3as.DivulgacaoONGs.Domain.Entities;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IPostgresRepository<User>, IDisposable
    {
    }
}
