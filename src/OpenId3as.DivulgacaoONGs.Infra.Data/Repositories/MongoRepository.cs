using MongoDB.Driver;
using OpenId3as.DivulgacaoONGs.Domain.Interfaces.Repositories;
using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Repositories
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class
    {
        protected readonly NOGContext _mongoContext;

        public MongoRepository(NOGContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public void Add(TEntity obj)
        {
            _mongoContext.Db.GetCollection<TEntity>(typeof(TEntity).Name).InsertOneAsync(obj);
        }

        public void AddRange(IEnumerable<TEntity> obj)
        {
            _mongoContext.Db.GetCollection<TEntity>(typeof(TEntity).Name).InsertManyAsync(obj);
        }

        public void Update(TEntity obj, long id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            _mongoContext.Db.GetCollection<TEntity>(typeof(TEntity).Name).ReplaceOne(filter, obj);
        }

        public void Delete(long id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            _mongoContext.Db.GetCollection<TEntity>(typeof(TEntity).Name).DeleteOne(filter);
        }

        public TEntity GetById(long id)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", id);
            return _mongoContext.Db.GetCollection<TEntity>(typeof(TEntity).Name).FindSync<TEntity>(filter).SingleOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _mongoContext.Db.GetCollection<TEntity>(typeof(TEntity).Name).Find<TEntity>(Builders<TEntity>.Filter.Empty).ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
