using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Institutions
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionService(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }

        public Institution Add(Institution institution)
        {
            return _institutionRepository.Add(institution);
        }

        public void Delete(long id)
        {
            _institutionRepository.Delete(id);
        }

        public void Dispose()
        {
            _institutionRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Institution> GetAll()
        {
            return _institutionRepository.GetAll();
        }

        public Institution GetById(long id)
        {
            return _institutionRepository.GetById(id);
        }

        public Institution Update(Institution institution)
        {
            return _institutionRepository.Update(institution);
        }
    }
}
