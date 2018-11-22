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
    public class HowToHelpAppService : IHowToHelpAppService
    {
        private readonly IMapper _mapper;
        private readonly IHowToHelpService _howToHelpService;
        private readonly IUnitOfWork _uow;

        public HowToHelpAppService(IMapper mapper, IHowToHelpService howToHelpService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _howToHelpService = howToHelpService;
            _uow = uow;
        }

        public HowToHelpViewModel Add(HowToHelpViewModel howToHelpViewModel)
        {
            var howToHelp = _mapper.Map<HowToHelpViewModel, HowToHelp>(howToHelpViewModel);
            _howToHelpService.Add(howToHelp);
            return howToHelpViewModel;
        }

        public void Delete(Int64 id)
        {
            _howToHelpService.Delete(id);
        }

        public void Dispose()
        {
            _howToHelpService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<HowToHelpViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<HowToHelp>, IEnumerable<HowToHelpViewModel>>(_howToHelpService.GetAll());
        }

        public HowToHelpViewModel GetById(Int64 id)
        {
            return _mapper.Map<HowToHelp, HowToHelpViewModel>(_howToHelpService.GetById(id));
        }

        public HowToHelpViewModel Update(HowToHelpViewModel howToHelpViewModel)
        {
            var howToHelp = _mapper.Map<HowToHelpViewModel, HowToHelp>(howToHelpViewModel);
            _howToHelpService.Update(howToHelp, howToHelp.Id);
            return howToHelpViewModel;
        }
    }
}
