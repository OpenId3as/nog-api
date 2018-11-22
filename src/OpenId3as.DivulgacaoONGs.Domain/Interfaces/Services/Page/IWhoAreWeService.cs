using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IWhoAreWeService : IDisposable
    {
        void Add(WhoAreWe whoAreWe);
        void AddRange(IEnumerable<WhoAreWe> whoAreWe);
        void Update(WhoAreWe whoAreWe, Int64 id);
        void Delete(Int64 id);
        WhoAreWe GetById(Int64 id);
        IEnumerable<WhoAreWe> GetAll();
    }
}
