using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface ICollaboratorAppService : IDisposable
    {
        CollaboratorViewModel Add(CollaboratorViewModel collaboratorViewModel);
        CollaboratorViewModel Update(CollaboratorViewModel collaboratorViewModel);
        CollaboratorViewModel GetById(long id);
        IEnumerable<CollaboratorViewModel> GetAll();
        CollaboratorViewModel GetInstitutionByLanguage(string language, string institution);
        void Delete(long id);
    }
}
