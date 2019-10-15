using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IHomeAppService : IDisposable
    {
        HomeViewModel Add(HomeViewModel homeViewModel);
        HomeViewModel Update(HomeViewModel homeViewModel);
        HomeViewModel GetById(long id);
        IEnumerable<HomeViewModel> GetAll();
        HomeViewModel GetInstitutionByLanguage(string language, string institution);
        void Delete(long id);
    }
}
