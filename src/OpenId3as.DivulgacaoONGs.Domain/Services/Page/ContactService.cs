using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Services.Page
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Add(Contact contact)
        {
            _contactRepository.Add(contact);
        }

        public void AddRange(IEnumerable<Contact> contact)
        {
            _contactRepository.AddRange(contact);
        }

        public void Delete(long id)
        {
            _contactRepository.Delete(id);
        }

        public void Dispose()
        {
            _contactRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contactRepository.GetAll();
        }

        public Contact GetById(long id)
        {
            return _contactRepository.GetById(id);
        }

        public Contact GetInstitutionByLanguage(string language, string institution)
        { 
            return _contactRepository.GetInstitutionByLanguage(language, institution);
        }

        public void Update(Contact contact, long id)
        {
            _contactRepository.Update(contact, id);
        }
    }
}
