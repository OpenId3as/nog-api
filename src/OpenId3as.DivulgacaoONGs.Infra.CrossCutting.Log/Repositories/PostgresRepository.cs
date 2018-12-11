using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Context;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Repositories
{
    public class PostgresRepository<TEntity> : IPostgresRepository<TEntity> where TEntity : class
    {
        protected LogContext Db;
        protected DbSet<TEntity> DbSet;

        public PostgresRepository(LogContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity obj)
        {
            obj = DbSet.Add(obj).Entity;
            SaveChanges();
            return obj;
        }

        public virtual void Delete(long id)
        {
            DbSet.Remove(DbSet.Find(id));
            SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            //return DbSet.Take(t).Skip(s).ToList();
            return DbSet.ToList();
        }

        public virtual TEntity GetById(long id)
        {
            return DbSet.Find(id);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public IEnumerable<TEntity> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public TEntity Update(TEntity obj)
        {
            var entry = Db.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
            SaveChanges();
            return obj;
        }
    }
}
