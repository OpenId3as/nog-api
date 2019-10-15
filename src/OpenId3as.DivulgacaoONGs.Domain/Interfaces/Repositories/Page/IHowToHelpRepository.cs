using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page
{
    public interface IHowToHelpRepository : IMongoRepository<HowToHelp>, IDisposable
    {
        HowToHelp GetInstitutionByLanguage(string language, string institution);
    }
}
