using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using System;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page
{
    public interface ILanguageRepository : IMongoRepository<Language>, IDisposable
    {
        Language GetByLang(string Lang);
    }
}
