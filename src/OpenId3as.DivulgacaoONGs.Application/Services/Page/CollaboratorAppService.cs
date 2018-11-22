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
    public class CollaboratorAppService : ICollaboratorAppService
    {
        private readonly IMapper _mapper;
        private readonly ICollaboratorService _collaboratorService;
        private readonly IUnitOfWork _uow;

        public CollaboratorAppService(IMapper mapper, ICollaboratorService collaboratorService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _collaboratorService = collaboratorService;
            _uow = uow;
        }

        public CollaboratorViewModel Add(CollaboratorViewModel collaboratorViewModel)
        {
            var collaborator = _mapper.Map<CollaboratorViewModel, Collaborator>(collaboratorViewModel);
            _collaboratorService.Add(collaborator);
            return collaboratorViewModel;
        }

        public void Delete(Int64 id)
        {
            _collaboratorService.Delete(id);
        }

        public void Dispose()
        {
            _collaboratorService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<CollaboratorViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Collaborator>, IEnumerable<CollaboratorViewModel>>(_collaboratorService.GetAll());
        }

        public CollaboratorViewModel GetById(Int64 id)
        {
            return _mapper.Map<Collaborator, CollaboratorViewModel>(_collaboratorService.GetById(id));
        }

        public CollaboratorViewModel Update(CollaboratorViewModel collaboratorViewModel)
        {
            var collaborator = _mapper.Map<CollaboratorViewModel, Collaborator>(collaboratorViewModel);
            _collaboratorService.Update(collaborator, collaborator.Id);
            return collaboratorViewModel;
        }
    }
}
