using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public void Add(Banner banner)
        {
            _bannerRepository.Add(banner);
        }

        public void AddRange(IEnumerable<Banner> banner)
        {
            _bannerRepository.AddRange(banner);
        }

        public void Delete(long id)
        {
            _bannerRepository.Delete(id);
        }

        public void Dispose()
        {
            _bannerRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Banner> GetAll()
        {
            return _bannerRepository.GetAll();
        }

        public Banner GetById(long id)
        {
            return _bannerRepository.GetById(id);
        }

        public void Update(Banner banner, long id)
        {
            _bannerRepository.Update(banner, id);
        }
    }
}
