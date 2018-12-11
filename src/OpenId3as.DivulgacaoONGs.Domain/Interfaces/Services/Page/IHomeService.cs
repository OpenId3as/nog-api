using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IHomeService : IDisposable
    {
        void Add(Home home);
        void AddRange(IEnumerable<Home> home);
        void Update(Home home, long id);
        void Delete(long id);
        Home GetById(long id);
        IEnumerable<Home> GetAll();
    }
}
