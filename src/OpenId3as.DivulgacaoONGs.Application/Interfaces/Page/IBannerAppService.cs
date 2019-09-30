using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IBannerAppService : IDisposable
    {
        BannerViewModel Add(BannerViewModel bannerViewModel);
        BannerViewModel Update(BannerViewModel bannerViewModel);
        BannerViewModel GetById(long id);
        BannerViewModel GetByInstitution(string institution);
        IEnumerable<BannerViewModel> GetAll();
        void Delete(long id);
    }
}
