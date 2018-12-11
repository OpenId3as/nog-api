using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface IMongoRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> obj);
        void Update(TEntity obj, long id);
        void Delete(long id);
        TEntity GetById(long id);
        IEnumerable<TEntity> GetAll();
    }
}
