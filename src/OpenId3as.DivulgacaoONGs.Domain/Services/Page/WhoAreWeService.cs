using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class WhoAreWeService : IWhoAreWeService
    {
        private readonly IWhoAreWeRepository _whoAreWeRepository;
        public WhoAreWeService(IWhoAreWeRepository whoAreWeRepository)
        {
            _whoAreWeRepository = whoAreWeRepository;
        }

        public void Add(WhoAreWe whoAreWe)
        {
            _whoAreWeRepository.Add(whoAreWe);
        }

        public void AddRange(IEnumerable<WhoAreWe> whoAreWe)
        {
            _whoAreWeRepository.AddRange(whoAreWe);
        }

        public void Delete(long id)
        {
            _whoAreWeRepository.Delete(id);
        }

        public void Dispose()
        {
            _whoAreWeRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<WhoAreWe> GetAll()
        {
            return _whoAreWeRepository.GetAll();
        }

        public WhoAreWe GetById(long id)
        {
            return _whoAreWeRepository.GetById(id);
        }

        public void Update(WhoAreWe whoAreWe, long id)
        {
            _whoAreWeRepository.Update(whoAreWe, id);
        }
    }
}
