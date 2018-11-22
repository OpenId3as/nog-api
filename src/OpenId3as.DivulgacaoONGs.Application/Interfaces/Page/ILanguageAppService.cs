using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface ILanguageAppService : IDisposable
    {
        LanguageViewModel Add(LanguageViewModel languageViewModel);
        LanguageViewModel Update(LanguageViewModel languageViewModel);
        LanguageViewModel GetById(Int64 id);
        IEnumerable<LanguageViewModel> GetAll();
        void Delete(Int64 id);
    }
}
