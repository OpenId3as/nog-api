using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class HowToHelpRepository : MongoRepository<HowToHelp>, IHowToHelpRepository
    {
        private IMongoCollection<HowToHelp> HowToHelp { get; }
        public HowToHelpRepository(NOGContext context)
            : base(context)
        {
            HowToHelp = _mongoContext.Db.GetCollection<HowToHelp>("HowToHelp");
        }

        public HowToHelp GetInstitutionByLanguage(string language, string institution)
        {
            return HowToHelp.FindSync(x => x.Language.Any(c => c.Lang == language) && x.Institution == institution).SingleOrDefault();
        }
    }
}
