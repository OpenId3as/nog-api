﻿using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page
{
    public interface IContactRepository : IMongoRepository<Contact>, IDisposable
    {
        Contact GetInstitutionByLanguage(string language, string institution);
    }
}
