using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class HomeRepository : MongoRepository<Home>, IHomeRepository
    {
        private IMongoCollection<Home> Home { get; }
        public HomeRepository(NOGContext context)
            : base(context)
        {
            Home = _mongoContext.Db.GetCollection<Home>("Home");
        }

        public Home GetInstitutionByLanguage(string language, string institution)
        {
            return Home.FindSync(x => x.Language.Any(c => c.Lang == language) && x.Institution == institution).SingleOrDefault();
        }
    }
}
