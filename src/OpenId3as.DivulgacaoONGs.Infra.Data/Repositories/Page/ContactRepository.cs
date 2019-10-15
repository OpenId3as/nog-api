using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories.Page;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class ContactRepository : MongoRepository<Contact>, IContactRepository
    {
        private IMongoCollection<Contact> Contact { get; }
        public ContactRepository(NOGContext context)
            : base(context)
        {
            Contact = _mongoContext.Db.GetCollection<Contact>("Contact");
        }

        public Contact GetInstitutionByLanguage(string language, string institution)
        {
            return Contact.FindSync(x => x.Language.Any(c => c.Lang == language) && x.Institution == institution).SingleOrDefault();
        }
    }
}
