using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface ILanguageService : IDisposable
    {
        void Add(Language language);
        void AddRange(IEnumerable<Language> language);
        void Update(Language language, long id);
        void Delete(long id);
        Language GetById(long id);
        Language GetByLang(string lang);
        IEnumerable<Language> GetAll();
    }
}
