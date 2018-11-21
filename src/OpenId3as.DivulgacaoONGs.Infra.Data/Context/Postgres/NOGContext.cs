using Microsoft.EntityFrameworkCore;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Collaborartors;
using OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Institutions;
using OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Volunteers;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.Context.Postgres
{
    public class NOGContext : DbContext
    {
        public NOGContext(DbContextOptions<NOGContext> options) : base(options)
        {

        }

        DbSet<Collaborator> Collaborator { get; set; }
        DbSet<CollaboratorAddress> CollaboratorAddresses { get; set; }
        DbSet<CollaboratorPhone> CollaboratorPhones { get; set; }
        DbSet<Institution> Institution { get; set; }
        DbSet<InstitutionAddress> InstitutionAddresses { get; set; }
        DbSet<InstitutionPhone> InstitutionPhones { get; set; }
        DbSet<Volunteer> Volunteer { get; set; }
        DbSet<VolunteerAddress> VolunteerAddresses { get; set; }
        DbSet<VolunteerPhone> VolunteerPhones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CollaboratorConfig());
            modelBuilder.ApplyConfiguration(new CollaboratorAddressConfig());
            modelBuilder.ApplyConfiguration(new CollaboratorPhoneConfig());
            modelBuilder.ApplyConfiguration(new InstitutionConfig());
            modelBuilder.ApplyConfiguration(new InstitutionAddressConfig());
            modelBuilder.ApplyConfiguration(new InstitutionPhoneConfig());
            modelBuilder.ApplyConfiguration(new VolunteerConfig());
            modelBuilder.ApplyConfiguration(new VolunteerAddressConfig());
            modelBuilder.ApplyConfiguration(new VolunteerPhoneConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
