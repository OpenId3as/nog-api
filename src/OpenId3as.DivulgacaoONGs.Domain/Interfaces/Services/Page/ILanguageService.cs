using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface ILanguageService : IDisposable
    {
        void Add(Language language);
        void AddRange(IEnumerable<Language> language);
        void Update(Language language, Int64 id);
        void Delete(Int64 id);
        Language GetById(Int64 id);
        IEnumerable<Language> GetAll();
    }
}
