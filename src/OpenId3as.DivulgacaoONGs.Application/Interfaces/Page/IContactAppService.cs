using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IContactAppService : IDisposable
    {
        ContactViewModel Add(ContactViewModel contactViewModel);
        ContactViewModel Update(ContactViewModel contactViewModel);
        ContactViewModel GetById(Int64 id);
        IEnumerable<ContactViewModel> GetAll();
        void Delete(Int64 id);
    }
}
