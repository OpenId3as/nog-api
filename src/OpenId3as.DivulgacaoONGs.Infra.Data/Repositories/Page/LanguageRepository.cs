using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class LanguageRepository : MongoRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(NOGContext context)
            : base(context)
        {

        }

        public Language GetByLang(string Lang)
        {
            var filter = Builders<Language>.Filter.Eq("Lang", Lang);
            return _mongoContext.Db.GetCollection<Language>("Language").FindSync<Language>(filter).SingleOrDefault();
        }
    }
}
