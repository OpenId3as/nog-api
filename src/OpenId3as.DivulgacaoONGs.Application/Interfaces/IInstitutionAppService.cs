using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
{
    public interface IInstitutionAppService : IDisposable
    {
        InstitutionViewModel Add(InstitutionViewModel institutionViewModel);
        InstitutionViewModel Update(InstitutionViewModel institutionViewModel);
        InstitutionViewModel GetById(Int64 id);
        IEnumerable<InstitutionViewModel> GetAll();
        void Delete(Int64 id);
    }
}
