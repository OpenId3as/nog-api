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
    public class ContactAppService : IContactAppService
    {
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;
        private readonly IUnitOfWork _uow;

        public ContactAppService(IMapper mapper, IContactService contactService, IUnitOfWork uow)
        {
            _mapper = mapper;
            _contactService = contactService;
            _uow = uow;
        }

        public ContactViewModel Add(ContactViewModel contactViewModel)
        {
            var contact = _mapper.Map<ContactViewModel, Contact>(contactViewModel);
            _contactService.Add(contact);
            return contactViewModel;
        }

        public void Delete(Int64 id)
        {
            _contactService.Delete(id);
        }

        public void Dispose()
        {
            _contactService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<ContactViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactViewModel>>(_contactService.GetAll());
        }

        public ContactViewModel GetById(Int64 id)
        {
            return _mapper.Map<Contact, ContactViewModel>(_contactService.GetById(id));
        }

        public ContactViewModel Update(ContactViewModel contactViewModel)
        {
            var contact = _mapper.Map<ContactViewModel, Contact>(contactViewModel);
            _contactService.Update(contact, contact.Id);
            return contactViewModel;
        }
    }
}
