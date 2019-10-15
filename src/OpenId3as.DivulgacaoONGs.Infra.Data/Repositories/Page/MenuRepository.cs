using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class MenuRepository : MongoRepository<Menu>, IMenuRepository
    {
        private IMongoCollection<Menu> Menu { get; }
        public MenuRepository(NOGContext context)
            : base(context)
        {
            Menu = _mongoContext.Db.GetCollection<Menu>("Menu");
        }

        public Menu GetInstitutionByLanguage(string language, string institution)
        {
            return Menu.FindSync(x => x.MenuItems.Any(c => c.Lang == language) && x.Institution == institution).SingleOrDefault();
        }
    }
}
