using AutoMapper;
using OpenId3as.DivulgacaoONGs.Application.Interfaces;
using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Services.Collaborators
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

            _uow.BeginTransaction();
            var collaboratorReturn = _collaboratorService.Add(collaborator);
            _uow.Commit();

            collaboratorViewModel = _mapper.Map<Collaborator, CollaboratorViewModel>(collaboratorReturn);
            return collaboratorViewModel;
        }

        public void Delete(long id)
        {
            _uow.BeginTransaction();
            _collaboratorService.Delete(id);
            _uow.Commit();
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

        public CollaboratorViewModel GetById(long id)
        {
            return _mapper.Map<Collaborator, CollaboratorViewModel>(_collaboratorService.GetById(id));
        }

        public CollaboratorViewModel Update(CollaboratorViewModel collaboratorViewModel)
        {
            _uow.BeginTransaction();
            _collaboratorService.Update(_mapper.Map<CollaboratorViewModel, Collaborator>(collaboratorViewModel));
            _uow.Commit();
            return collaboratorViewModel;
        }
    }
}
