using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class HowToHelpService : IHowToHelpService
    {
        private readonly IHowToHelpRepository _howToHelpRepository;
        public HowToHelpService(IHowToHelpRepository howToHelpRepository)
        {
            _howToHelpRepository = howToHelpRepository;
        }

        public void Add(HowToHelp howToHelp)
        {
            _howToHelpRepository.Add(howToHelp);
        }

        public void AddRange(IEnumerable<HowToHelp> howToHelp)
        {
            _howToHelpRepository.AddRange(howToHelp);
        }

        public void Delete(long id)
        {
            _howToHelpRepository.Delete(id);
        }

        public void Dispose()
        {
            _howToHelpRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<HowToHelp> GetAll()
        {
            return _howToHelpRepository.GetAll();
        }

        public HowToHelp GetById(long id)
        {
            return _howToHelpRepository.GetById(id);
        }

        public void Update(HowToHelp howToHelp, long id)
        {
            _howToHelpRepository.Update(howToHelp, id);
        }
    }
}
