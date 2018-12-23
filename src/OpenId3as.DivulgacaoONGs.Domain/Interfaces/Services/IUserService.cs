using OpenId3as.DivulgacaoONGs.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services
{
    public interface IUserService : IDisposable
    {
        User Add(User user);
        User Update(User user);
        User GetById(long id);
        IEnumerable<User> GetAll();
        void Delete(long id);
    }
}
