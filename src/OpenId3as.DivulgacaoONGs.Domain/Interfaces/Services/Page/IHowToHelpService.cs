using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IHowToHelpService : IDisposable
    {
        void Add(HowToHelp howToHelp);
        void AddRange(IEnumerable<HowToHelp> howToHelp);
        void Update(HowToHelp howToHelp, long id);
        void Delete(long id);
        HowToHelp GetById(long id);
        IEnumerable<HowToHelp> GetAll();
    }
}
