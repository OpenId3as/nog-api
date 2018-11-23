using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces
{
    public interface IPostgresRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(Int64 id);
        int SaveChanges();
        TEntity GetById(Int64 id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
    }
}
