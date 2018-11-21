using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services
{
    public interface ICollaboratorService : IDisposable
    {
        Collaborator Add(Collaborator collaborator);
        Collaborator Update(Collaborator collaborator);
        Collaborator GetById(Int64 id);
        IEnumerable<Collaborator> GetAll();
        void Delete(Int64 id);
    }
}
