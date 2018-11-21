using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface ICollaboratorRepository : IPostgresRepository<Collaborator>, IDisposable
    {
        
    }
}
