using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface ILogoService : IDisposable
    {
        void Add(Logo logo);
        void AddRange(IEnumerable<Logo> logo);
        void Update(Logo logo, long id);
        void Delete(long id);
        Logo GetById(long id);
        IEnumerable<Logo> GetAll();
    }
}
