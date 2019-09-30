using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.Interfaces.Page;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Services.Page
{
    public class LanguageAppService : ILanguageAppService
    {
        private readonly IMapper _mapper;
        private readonly ILanguageService _languageService;
        private readonly IUnitOfWork _uow;

        public LanguageAppService(IMapper mapper, ILanguageService languageService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _languageService = languageService;
            _uow = uow;
        }

        public LanguageViewModel Add(LanguageViewModel languageViewModel)
        {
            var language = _mapper.Map<LanguageViewModel, Language>(languageViewModel);
            _languageService.Add(language);
            return languageViewModel;
        }

        public void Delete(long id)
        {
            _languageService.Delete(id);
        }

        public void Dispose()
        {
            _languageService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<LanguageViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageViewModel>>(_languageService.GetAll());
        }

        public LanguageViewModel GetById(long id)
        {
            return _mapper.Map<Language, LanguageViewModel>(_languageService.GetById(id));
        }

        public LanguageViewModel GetByLang(string lang)
        {
            return _mapper.Map<Language, LanguageViewModel>(_languageService.GetByLang(lang));
        }

        public LanguageViewModel Update(LanguageViewModel languageViewModel)
        {
            var language = _mapper.Map<LanguageViewModel, Language>(languageViewModel);
            _languageService.Update(language, language.Id);
            return languageViewModel;
        }
    }
}
