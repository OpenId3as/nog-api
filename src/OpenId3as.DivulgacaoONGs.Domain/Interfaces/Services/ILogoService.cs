using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services
{
    public interface ILogoService : IDisposable
    {
        void Add(Logo logo);
        void AddRange(IEnumerable<Logo> logo);
        void Update(Logo logo, Int64 id);
        void Delete(Int64 id);
        Logo GetById(Int64 id);
        IEnumerable<Logo> GetAll();
    }
}
