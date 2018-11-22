using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IHomeService : IDisposable
    {
        void Add(Home home);
        void AddRange(IEnumerable<Home> home);
        void Update(Home home, Int64 id);
        void Delete(Int64 id);
        Home GetById(Int64 id);
        IEnumerable<Home> GetAll();
    }
}
