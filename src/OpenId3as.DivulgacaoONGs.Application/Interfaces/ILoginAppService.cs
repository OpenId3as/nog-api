using OpenId3as.DivulgacaoONGs.Application.ViewModels;
using System;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
{
    public interface ILoginAppService : IDisposable
    {
        object GetByLogin(UserViewModel userViewModel);
    }
}
