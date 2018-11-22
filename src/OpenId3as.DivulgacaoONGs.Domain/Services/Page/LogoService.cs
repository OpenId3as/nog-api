using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class LogoService : ILogoService
    {
        private readonly ILogoRepository _logoRepository;
        public LogoService(ILogoRepository logoRepository)
        {
            _logoRepository = logoRepository;
        }

        public void Add(Logo logo)
        {
            _logoRepository.Add(logo);
        }

        public void AddRange(IEnumerable<Logo> logo)
        {
            _logoRepository.AddRange(logo);
        }

        public void Delete(long id)
        {
            _logoRepository.Delete(id);
        }

        public void Dispose()
        {
            _logoRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Logo> GetAll()
        {
            return _logoRepository.GetAll();
        }

        public Logo GetById(Int64 id)
        {
            return _logoRepository.GetById(id);
        }

        public void Update(Logo logo, long id)
        {
            _logoRepository.Update(logo, id);
        }
    }
}
