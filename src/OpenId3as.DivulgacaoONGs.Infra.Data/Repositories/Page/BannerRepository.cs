using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class BannerRepository : MongoRepository<Banner>, IBannerRepository
    {
        public BannerRepository(NOGContext context)
            : base(context)
        {

        }

        public Banner GetByInstitution(string institution)
        {
            var filter = Builders<Banner>.Filter.Eq("Institution", institution);
            return _mongoContext.Db.GetCollection<Banner>("Banner").FindSync<Banner>(filter).SingleOrDefault();
        }
    }
}
