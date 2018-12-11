using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface IPostgresRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(long id);
        int SaveChanges();
        TEntity GetById(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate);
    }
}
