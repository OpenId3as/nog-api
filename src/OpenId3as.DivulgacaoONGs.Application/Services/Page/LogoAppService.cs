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
    public class LogoAppService : ILogoAppService
    {
        private readonly IMapper _mapper;
        private readonly ILogoService _logoService;
        private readonly IUnitOfWork _uow;

        public LogoAppService(IMapper mapper, ILogoService logoService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _logoService = logoService;
            _uow = uow;
        }

        public LogoViewModel Add(LogoViewModel logoViewModel)
        {
            var logo = _mapper.Map<LogoViewModel, Logo>(logoViewModel);
            logo.Id = new Logo().Id;
            _logoService.Add(logo);
            logoViewModel = _mapper.Map<Logo, LogoViewModel>(logo);
            return logoViewModel;
        }

        public void Delete(Int64 id)
        {
            _logoService.Delete(id);
        }

        public void Dispose()
        {
            _logoService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<LogoViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Logo>, IEnumerable<LogoViewModel>>(_logoService.GetAll());
        }

        public LogoViewModel GetById(Int64 id)
        {
            return _mapper.Map<Logo, LogoViewModel>(_logoService.GetById(id));
        }

        public LogoViewModel Update(LogoViewModel logoViewModel)
        {
            var logo = _mapper.Map<LogoViewModel, Logo>(logoViewModel);
            _logoService.Update(logo, logo.Id);
            return logoViewModel;
        }
    }
}
