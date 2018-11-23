using Model = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Entities;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces
{
    public interface ILogRepository : IPostgresRepository<Model.Log>
    {
    }
}
