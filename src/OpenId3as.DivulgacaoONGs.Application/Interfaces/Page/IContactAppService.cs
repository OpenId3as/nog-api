using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IContactAppService : IDisposable
    {
        ContactViewModel Add(ContactViewModel contactViewModel);
        ContactViewModel Update(ContactViewModel contactViewModel);
        ContactViewModel GetById(long id);
        IEnumerable<ContactViewModel> GetAll();
        ContactViewModel GetInstitutionByLanguage(string language, string institution);
        void Delete(long id);
    }
}
