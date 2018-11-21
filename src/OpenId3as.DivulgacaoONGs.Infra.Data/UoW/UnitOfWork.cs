using OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres;
using OpenId3as.DivulgacaoONGs.Infra.Data.Interfaces;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NOGContext _context;
        private bool _disposed;

        public UnitOfWork(NOGContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
