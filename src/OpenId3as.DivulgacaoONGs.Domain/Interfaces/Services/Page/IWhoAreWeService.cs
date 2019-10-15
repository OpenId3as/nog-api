using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IWhoAreWeService : IDisposable
    {
        void Add(WhoAreWe whoAreWe);
        void AddRange(IEnumerable<WhoAreWe> whoAreWe);
        void Update(WhoAreWe whoAreWe, long id);
        void Delete(long id);
        WhoAreWe GetById(long id);
        IEnumerable<WhoAreWe> GetAll();
        WhoAreWe GetInstitutionByLanguage(string language, string institution);
    }
}
