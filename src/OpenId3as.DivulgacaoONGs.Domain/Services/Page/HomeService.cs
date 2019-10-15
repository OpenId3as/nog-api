using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homeRepository;
        public HomeService(IHomeRepository homeRepository)
        {
            _homeRepository = homeRepository;
        }

        public void Add(Home home)
        {
            _homeRepository.Add(home);
        }

        public void AddRange(IEnumerable<Home> home)
        {
            _homeRepository.AddRange(home);
        }

        public void Delete(long id)
        {
            _homeRepository.Delete(id);
        }

        public void Dispose()
        {
            _homeRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Home> GetAll()
        {
            return _homeRepository.GetAll();
        }

        public Home GetById(long id)
        {
            return _homeRepository.GetById(id);
        }

        public Home GetInstitutionByLanguage(string language, string institution)
        {
            return _homeRepository.GetInstitutionByLanguage(language, institution);
        }

        public void Update(Home home, long id)
        {
            _homeRepository.Update(home, id);
        }
    }
}
