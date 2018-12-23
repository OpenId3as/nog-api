using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IMenuService : IDisposable
    {
        void Add(Menu menu);
        void AddRange(IEnumerable<Menu> menu);
        void Update(Menu menu, long id);
        void Delete(long id);
        Menu GetById(long id);
        IEnumerable<Menu> GetAll();
    }
}
