using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Services.Page
{
    public interface IHowToHelpService : IDisposable
    {
        void Add(HowToHelp howToHelp);
        void AddRange(IEnumerable<HowToHelp> howToHelp);
        void Update(HowToHelp howToHelp, Int64 id);
        void Delete(Int64 id);
        HowToHelp GetById(Int64 id);
        IEnumerable<HowToHelp> GetAll();
    }
}
