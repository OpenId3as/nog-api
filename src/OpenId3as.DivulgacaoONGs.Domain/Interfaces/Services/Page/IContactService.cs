using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IContactService : IDisposable
    {
        void Add(Contact contact);
        void AddRange(IEnumerable<Contact> contact);
        void Update(Contact contact, long id);
        void Delete(long id);
        Contact GetById(long id);
        IEnumerable<Contact> GetAll();
    }
}
