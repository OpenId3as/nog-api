using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services
{
    public interface ICollaboratorService : IDisposable
    {
        Collaborator Add(Collaborator collaborator);
        void Delete(long id);
        IEnumerable<Collaborator> FindWithPagedSearch(string sort = "", int limitRows = 50, int page = 0, string firstName = "", string lastName = "", string email = "", bool? active = null);
        IEnumerable<Collaborator> GetAll();
        int GetCountPagedSearch(string firstName = "", string lastName = "", string email = "", bool? active = null);
        Collaborator GetById(long id);
        Collaborator Update(Collaborator collaborator);
        
    }
}
