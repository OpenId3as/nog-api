﻿using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
{
    public interface IVolunteerAppService : IDisposable
    {
        VolunteerViewModel Add(VolunteerViewModel volunteerViewModel);
        VolunteerViewModel Update(VolunteerViewModel volunteerViewModel);
        VolunteerViewModel GetById(Int64 id);
        IEnumerable<VolunteerViewModel> GetAll();
        void Delete(Int64 id);
    }
}
