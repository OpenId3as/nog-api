using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface ICollaboratorService : IDisposable
    {
        void Add(Collaborator collaborator);
        void AddRange(IEnumerable<Collaborator> collaborator);
        void Update(Collaborator collaborator, long id);
        void Delete(long id);
        Collaborator GetById(long id);
        IEnumerable<Collaborator> GetAll();
    }
}
