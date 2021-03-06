﻿using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services
{
    public interface IInstitutionService : IDisposable
    {
        Institution Add(Institution institution);
        Institution Update(Institution institution);
        Institution GetById(long id);
        IEnumerable<Institution> GetAll();
        void Delete(long id);
    }
}
