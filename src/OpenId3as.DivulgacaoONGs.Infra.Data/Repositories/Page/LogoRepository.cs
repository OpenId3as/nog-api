using OpenId3as.DivulgacaoONGs.Domain.Entities.Page;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories.Page
{
    public class LogoRepository : MongoRepository<Logo>, ILogoRepository
    {
        public LogoRepository(NOGContext context)
            : base(context)
        {

        }
    }
}
