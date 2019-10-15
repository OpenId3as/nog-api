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
    public class WhoAreWeAppService : IWhoAreWeAppService
    {
        private readonly IMapper _mapper;
        private readonly IWhoAreWeService _whoAreWeService;
        private readonly IUnitOfWork _uow;

        public WhoAreWeAppService(IMapper mapper, IWhoAreWeService whoAreWeService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _whoAreWeService = whoAreWeService;
            _uow = uow;
        }

        public WhoAreWeViewModel Add(WhoAreWeViewModel whoAreWeViewModel)
        {
            var whoAreWe = _mapper.Map<WhoAreWeViewModel, WhoAreWe>(whoAreWeViewModel);
            _whoAreWeService.Add(whoAreWe);
            return whoAreWeViewModel;
        }

        public void Delete(long id)
        {
            _whoAreWeService.Delete(id);
        }

        public void Dispose()
        {
            _whoAreWeService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<WhoAreWeViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<WhoAreWe>, IEnumerable<WhoAreWeViewModel>>(_whoAreWeService.GetAll());
        }

        public WhoAreWeViewModel GetById(long id)
        {
            return _mapper.Map<WhoAreWe, WhoAreWeViewModel>(_whoAreWeService.GetById(id));
        }

        public WhoAreWeViewModel GetInstitutionByLanguage(string language, string institution)
        {
            return _mapper.Map<WhoAreWe, WhoAreWeViewModel>(_whoAreWeService.GetInstitutionByLanguage(language, institution));
        }

        public WhoAreWeViewModel Update(WhoAreWeViewModel whoAreWeViewModel)
        {
            var whoAreWe = _mapper.Map<WhoAreWeViewModel, WhoAreWe>(whoAreWeViewModel);
            _whoAreWeService.Update(whoAreWe, whoAreWe.Id);
            return whoAreWeViewModel;
        }
    }
}
