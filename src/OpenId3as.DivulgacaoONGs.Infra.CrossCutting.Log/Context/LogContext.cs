using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.EntityConfig;
using Model = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Entities;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Context
{
    public class LogContext : DbContext
    {
        private readonly string _cnString;
        public LogContext(string cnString)
        {
            _cnString = cnString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_cnString);
        }

        DbSet<Model.Log> Log { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LogConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
