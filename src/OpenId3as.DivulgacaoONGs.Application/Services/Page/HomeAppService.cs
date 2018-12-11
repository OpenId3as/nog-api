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
    public class HomeAppService : IHomeAppService
    {
        private readonly IMapper _mapper;
        private readonly IHomeService _logoService;
        private readonly IUnitOfWork _uow;

        public HomeAppService(IMapper mapper, IHomeService logoService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _logoService = logoService;
            _uow = uow;
        }

        public HomeViewModel Add(HomeViewModel logoViewModel)
        {
            var logo = _mapper.Map<HomeViewModel, Home>(logoViewModel);
            _logoService.Add(logo);
            return logoViewModel;
        }

        public void Delete(long id)
        {
            _logoService.Delete(id);
        }

        public void Dispose()
        {
            _logoService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<HomeViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Home>, IEnumerable<HomeViewModel>>(_logoService.GetAll());
        }

        public HomeViewModel GetById(long id)
        {
            return _mapper.Map<Home, HomeViewModel>(_logoService.GetById(id));
        }

        public HomeViewModel Update(HomeViewModel logoViewModel)
        {
            var logo = _mapper.Map<HomeViewModel, Home>(logoViewModel);
            _logoService.Update(logo, logo.Id);
            return logoViewModel;
        }
    }
}
