using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface ILogoAppService : IDisposable
    {
        LogoViewModel Add(LogoViewModel logoViewModel);
        LogoViewModel Update(LogoViewModel logoViewModel);
        LogoViewModel GetById(long id);
        IEnumerable<LogoViewModel> GetAll();
        void Delete(long id);
    }
}
