using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class CollaboratorRepository : MongoRepository<Collaborator>, ICollaboratorRepository
    {
        private IMongoCollection<Collaborator> Collaborator { get; }
        public CollaboratorRepository(NOGContext context)
            : base(context)
        {
            Collaborator = _mongoContext.Db.GetCollection<Collaborator>("Collaborator");
        }

        public Collaborator GetInstitutionByLanguage(string language, string institution)
        {
            return Collaborator.FindSync(x => x.Language.Any(c => c.Lang == language) && x.Institution == institution).SingleOrDefault();
        }
    }
}
