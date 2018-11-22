using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IHomeAppService : IDisposable
    {
        HomeViewModel Add(HomeViewModel homeViewModel);
        HomeViewModel Update(HomeViewModel homeViewModel);
        HomeViewModel GetById(Int64 id);
        IEnumerable<HomeViewModel> GetAll();
        void Delete(Int64 id);
    }
}
