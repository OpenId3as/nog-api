using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IMenuAppService : IDisposable
    {
        MenuViewModel Add(MenuViewModel menuViewModel);
        MenuViewModel Update(MenuViewModel menuViewModel);
        MenuViewModel GetById(long id);
        MenuViewModel GetInstitutionByLanguage(string language, string institution);
        IEnumerable<MenuViewModel> GetAll();
        void Delete(long id);
    }
}
