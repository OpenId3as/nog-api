using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IBannerService : IDisposable
    {
        void Add(Banner banner);
        void AddRange(IEnumerable<Banner> banner);
        void Update(Banner banner, long id);
        void Delete(long id);
        Banner GetById(long id);
        IEnumerable<Banner> GetAll();
    }
}
