using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        public CollaboratorService(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }

        public void Add(Collaborator collaborator)
        {
            _collaboratorRepository.Add(collaborator);
        }

        public void AddRange(IEnumerable<Collaborator> collaborator)
        {
            _collaboratorRepository.AddRange(collaborator);
        }

        public void Delete(long id)
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

        public Collaborator GetById(long id)
        {
            return _collaboratorRepository.GetById(id);
        }

        public Collaborator GetInstitutionByLanguage(string language, string institution)
        {
            return _collaboratorRepository.GetInstitutionByLanguage(language, institution);
        }

        public void Update(Collaborator collaborator, long id)
        {
            _collaboratorRepository.Update(collaborator, id);
        }
    }
}
