using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IWhoAreWeAppService : IDisposable
    {
        WhoAreWeViewModel Add(WhoAreWeViewModel whoAreWeViewModel);
        WhoAreWeViewModel Update(WhoAreWeViewModel whoAreWeViewModel);
        WhoAreWeViewModel GetById(Int64 id);
        IEnumerable<WhoAreWeViewModel> GetAll();
        void Delete(Int64 id);
    }
}
