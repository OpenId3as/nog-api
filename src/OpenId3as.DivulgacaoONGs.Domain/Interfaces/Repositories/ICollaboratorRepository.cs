using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface ICollaboratorRepository : IPostgresRepository<Collaborator>, IDisposable
    {
        IEnumerable<Collaborator> FindWithPagedSearch(string sort = "", int limitRows = 50, int page = 0, string firstName = "", string lastName = "", string email = "", bool? active = null);
        int GetCountPagedSearch(string firstName = "", string lastName = "", string email = "", bool? active = null);
    }
}
