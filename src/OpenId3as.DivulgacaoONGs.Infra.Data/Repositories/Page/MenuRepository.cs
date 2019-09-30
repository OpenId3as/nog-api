using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class MenuRepository : MongoRepository<Menu>, IMenuRepository
    {
        public MenuRepository(NOGContext context)
            : base(context)
        {

        }

        public Menu GetByInstitution(string institution)
        {
            var filter = Builders<Menu>.Filter.Eq("Institution", institution);
            return _mongoContext.Db.GetCollection<Menu>("Menu").FindSync<Menu>(filter).SingleOrDefault();
        }
    }
}
