using OpenId3as.DivulgacaoONGs.Application.DataTransferObject.PagedSearch;
using OpenId3as.DivulgacaoONGs.Application.ValueObjects.HATEOAS;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
{
    public interface ICollaboratorAppService : IDisposable
    {
        CollaboratorViewModel Add(CollaboratorViewModel collaboratorViewModel);
        void Delete(long id);
        PagedSearch<CollaboratorViewModel> FindWithPagedSearch(List<SortItemDTO> sort, int limitRows = 50, int page = 0, string firstName = "", string lastName = "", string email = "", bool? active = null);
        CollaboratorViewModel GetById(long id);
        IEnumerable<CollaboratorViewModel> GetAll();
        CollaboratorViewModel Update(CollaboratorViewModel collaboratorViewModel);
    }
}
