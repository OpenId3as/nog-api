using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IHowToHelpAppService : IDisposable
    {
        HowToHelpViewModel Add(HowToHelpViewModel howToHelpViewModel);
        HowToHelpViewModel Update(HowToHelpViewModel howToHelpViewModel);
        HowToHelpViewModel GetById(long id);
        IEnumerable<HowToHelpViewModel> GetAll();
        HowToHelpViewModel GetInstitutionByLanguage(string language, string institution);
        void Delete(long id);
    }
}
