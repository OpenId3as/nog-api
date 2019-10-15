using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IWhoAreWeAppService : IDisposable
    {
        WhoAreWeViewModel Add(WhoAreWeViewModel whoAreWeViewModel);
        WhoAreWeViewModel Update(WhoAreWeViewModel whoAreWeViewModel);
        WhoAreWeViewModel GetById(long id);
        IEnumerable<WhoAreWeViewModel> GetAll();
        WhoAreWeViewModel GetInstitutionByLanguage(string language, string institution);
        void Delete(long id);
    }
}
