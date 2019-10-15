using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class WhoAreWeRepository : MongoRepository<WhoAreWe>, IWhoAreWeRepository
    {
        private IMongoCollection<WhoAreWe> WhoAreWe { get; }
        public WhoAreWeRepository(NOGContext context)
            : base(context)
        {
            WhoAreWe = _mongoContext.Db.GetCollection<WhoAreWe>("WhoAreWe");
        }

        public WhoAreWe GetInstitutionByLanguage(string language, string institution)
        {
            return WhoAreWe.FindSync(x => x.Language.Any(c => c.Lang == language) && x.Institution == institution).SingleOrDefault();
        }
    }
}
