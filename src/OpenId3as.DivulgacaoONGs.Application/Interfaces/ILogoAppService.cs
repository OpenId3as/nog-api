using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
{
    public interface ILogoAppService : IDisposable
    {
        LogoViewModel Add(LogoViewModel logoViewModel);
        LogoViewModel Update(LogoViewModel logoViewModel);
        LogoViewModel GetById(Int64 id);
        IEnumerable<LogoViewModel> GetAll();
        void Delete(Int64 id);
        
    }
}
