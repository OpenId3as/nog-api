using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Collaborators
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;

        public CollaboratorService(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }

        public Collaborator Add(Collaborator collaborator)
        {
            return _collaboratorRepository.Add(collaborator);
        }

        public void Delete(Int64 id)
        {
            _collaboratorRepository.Delete(id);
        }

        public void Dispose()
        {
            _collaboratorRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Collaborator> GetAll()
        {
            return _collaboratorRepository.GetAll();
        }

        public Collaborator GetById(Int64 id)
        {
            return _collaboratorRepository.GetById(id);
        }

        public Collaborator Update(Collaborator collaborator)
        {
            return _collaboratorRepository.Update(collaborator);
        }
    }
}
