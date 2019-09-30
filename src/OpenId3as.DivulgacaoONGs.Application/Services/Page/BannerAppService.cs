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
    public class BannerAppService : IBannerAppService
    {
        private readonly IMapper _mapper;
        private readonly IBannerService _bannerService;
        private readonly IUnitOfWork _uow;

        public BannerAppService(IMapper mapper, IBannerService bannerService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _bannerService = bannerService;
            _uow = uow;
        }

        public BannerViewModel Add(BannerViewModel bannerViewModel)
        {
            var banner = _mapper.Map<BannerViewModel, Banner>(bannerViewModel);
            _bannerService.Add(banner);
            return bannerViewModel;
        }

        public void Delete(long id)
        {
            _bannerService.Delete(id);
        }

        public void Dispose()
        {
            _bannerService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<BannerViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Banner>, IEnumerable<BannerViewModel>>(_bannerService.GetAll());
        }

        public BannerViewModel GetById(long id)
        {
            return _mapper.Map<Banner, BannerViewModel>(_bannerService.GetById(id));
        }

        public BannerViewModel GetByInstitution(string intitution)
        {
            return _mapper.Map<Banner, BannerViewModel>(_bannerService.GetByInstitution(intitution));
        }

        public BannerViewModel Update(BannerViewModel bannerViewModel)
        {
            var banner = _mapper.Map<BannerViewModel, Banner>(bannerViewModel);
            _bannerService.Update(banner, banner.Id);
            return bannerViewModel;
        }
    }
}
