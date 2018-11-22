using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IContactService : IDisposable
    {
        void Add(Contact contact);
        void AddRange(IEnumerable<Contact> contact);
        void Update(Contact contact, Int64 id);
        void Delete(Int64 id);
        Contact GetById(Int64 id);
        IEnumerable<Contact> GetAll();
    }
}
