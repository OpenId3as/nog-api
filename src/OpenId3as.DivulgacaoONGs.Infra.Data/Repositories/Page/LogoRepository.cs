using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class LogoRepository : MongoRepository<Logo>, ILogoRepository
    {
        private IMongoCollection<Logo> Logo { get; }
        public LogoRepository(NOGContext context)
            : base(context)
        {
            Logo = _mongoContext.Db.GetCollection<Logo>("Logo");
        }

        public Logo GetByInstitution(string institution)
        {
            return Logo.FindSync(x => x.Institution == institution).SingleOrDefault();
        }
    }
}
