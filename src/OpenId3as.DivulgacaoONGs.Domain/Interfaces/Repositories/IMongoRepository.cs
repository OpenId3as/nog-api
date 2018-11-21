using System;
using System.Collections.Generic;

namespace OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories
{
    public interface IMongoRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void AddRange(IEnumerable<TEntity> obj);
        void Update(TEntity obj, Int64 id);
        void Delete(Int64 id);
        TEntity GetById(Int64 id);
        IEnumerable<TEntity> GetAll();
    }
}
