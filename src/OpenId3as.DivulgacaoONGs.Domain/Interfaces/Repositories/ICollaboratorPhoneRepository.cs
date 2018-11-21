using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface ICollaboratorPhoneRepository : IPostgresRepository<CollaboratorPhone>, IDisposable
    {
    }
}
