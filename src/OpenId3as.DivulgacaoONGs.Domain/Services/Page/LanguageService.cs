using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public void Add(Language language)
        {
            _languageRepository.Add(language);
        }

        public void AddRange(IEnumerable<Language> language)
        {
            _languageRepository.AddRange(language);
        }

        public void Delete(long id)
        {
            _languageRepository.Delete(id);
        }

        public void Dispose()
        {
            _languageRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Language> GetAll()
        {
            return _languageRepository.GetAll();
        }

        public Language GetById(long id)
        {
            return _languageRepository.GetById(id);
        }

        public Language GetByLang(string lang)
        {
            return _languageRepository.GetByLang(lang);
        }

        public void Update(Language language, long id)
        {
            _languageRepository.Update(language, id);
        }
    }
}
