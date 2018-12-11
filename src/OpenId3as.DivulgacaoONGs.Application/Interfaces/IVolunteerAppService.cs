using OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Application.Interfaces
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
