﻿using OpenId3as.DivulgacaoONGs.Application.ViewModels.Collaborators;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
{
    public interface ICollaboratorAppService : IDisposable
    {
        CollaboratorViewModel Add(CollaboratorViewModel collaboratorViewModel);
        CollaboratorViewModel Update(CollaboratorViewModel collaboratorViewModel);
        CollaboratorViewModel GetById(Int64 id);
        IEnumerable<CollaboratorViewModel> GetAll();
        void Delete(Int64 id);
    }
}