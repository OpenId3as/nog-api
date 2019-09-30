using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class LogoRepository : MongoRepository<Logo>, ILogoRepository
    {
        public LogoRepository(NOGContext context)
            : base(context)
        {

        }

        public Logo GetByInstitution(string institution)
        {
            var filter = Builders<Logo>.Filter.Eq("Institution", institution);
            return _mongoContext.Db.GetCollection<Logo>("Logo").FindSync<Logo>(filter).SingleOrDefault();
        }
    }
}
