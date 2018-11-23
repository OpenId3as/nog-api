using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Context;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using Model = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Entities;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Repositories
{
    public class LogRepository : PostgresRepository<Model.Log>, ILogRepository
    {
        public LogRepository(LogContext context)
            : base(context)
        {

        }
    }
}
