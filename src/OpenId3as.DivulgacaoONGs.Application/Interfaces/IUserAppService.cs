using OpenId3as.DivulgacaoONGs.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
{
    public interface IUserAppService : IDisposable
    {
        UserViewModel Add(UserViewModel userViewModel);
        UserViewModel Update(UserViewModel userViewModel);
        UserViewModel GetById(long id);
        IEnumerable<UserViewModel> GetAll();
        void Delete(long id);
    }
}
