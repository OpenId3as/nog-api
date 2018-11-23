using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Collaborartors
{
    public class CollaboratorConfig : IEntityTypeConfiguration<Collaborator>
    {
        public void Configure(EntityTypeBuilder<Collaborator> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Birth)
                .IsRequired();

            builder.Property(c => c.Created)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Updated);

            builder.Property(c => c.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany<CollaboratorAddress>(x => x.Addresses).WithOne(x => x.Collaborator);
            builder.HasMany<CollaboratorPhone>(x => x.Phones).WithOne(x => x.Collaborator);

            builder.ToTable("collaborator", schema: "nog");
        }
    }
}