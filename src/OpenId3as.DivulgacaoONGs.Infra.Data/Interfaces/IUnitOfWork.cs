namespace OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
    }
}
