using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using System;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Institutions;
using System.Collections.Generic;
using AutoMapper;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;

namespace OpenId3as.DivulgacaoONGs.Application.Services.Institutions
{
    public class InstitutionAppService : IInstitutionAppService
    {
        private readonly IMapper _mapper;
        private readonly IInstitutionService _institutionService;
        private readonly IUnitOfWork _uow;

        public InstitutionAppService(IMapper mapper, IInstitutionService institutionService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _institutionService = institutionService;
            _uow = uow;
        }

        public InstitutionViewModel Add(InstitutionViewModel institutionViewModel)
        {
            var institution = _mapper.Map<InstitutionViewModel, Institution>(institutionViewModel);

            _uow.BeginTransaction();
            var institutionReturn = _institutionService.Add(institution);
            _uow.Commit();

            institutionViewModel = _mapper.Map<Institution, InstitutionViewModel>(institutionReturn);
            return institutionViewModel;
        }

        public void Delete(Int64 id)
        {
            _uow.BeginTransaction();
            _institutionService.Delete(id);
            _uow.Commit();
        }

        public void Dispose()
        {
            _institutionService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<InstitutionViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Institution>, IEnumerable<InstitutionViewModel>>(_institutionService.GetAll());
        }

        public InstitutionViewModel GetById(Int64 id)
        {
            return _mapper.Map<Institution, InstitutionViewModel>(_institutionService.GetById(id));
        }

        public InstitutionViewModel Update(InstitutionViewModel institutionViewModel)
        {
            _uow.BeginTransaction();
            _institutionService.Update(_mapper.Map<InstitutionViewModel, Institution>(institutionViewModel));
            _uow.Commit();
            return institutionViewModel;
        }
    }
}
