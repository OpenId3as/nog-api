using OpenId3as.DivulgacaoONGs.Application.ViewModels.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces.Page
{
    public interface IVolunteerAppService : IDisposable
    {
        VolunteerViewModel Add(VolunteerViewModel volunteerViewModel);
        VolunteerViewModel Update(VolunteerViewModel volunteerViewModel);
        VolunteerViewModel GetById(long id);
        IEnumerable<VolunteerViewModel> GetAll();
        void Delete(long id);
    }
}
