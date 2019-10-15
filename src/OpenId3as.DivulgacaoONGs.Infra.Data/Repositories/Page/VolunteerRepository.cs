using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class VolunteerRepository : MongoRepository<Volunteer>, IVolunteerRepository
    {
        private IMongoCollection<Volunteer> Volunteer { get; }
        public VolunteerRepository(NOGContext context)
            : base(context)
        {
            Volunteer = _mongoContext.Db.GetCollection<Volunteer>("Volunteer");
        }

        public Volunteer GetInstitutionByLanguage(string language, string institution)
        {
            return Volunteer.FindSync(x => x.Language.Any(c => c.Lang == language) && x.Institution == institution).SingleOrDefault();
        }
    }
}
