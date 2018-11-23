using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Collaborartors
{
    public class CollaboratorAddressConfig : IEntityTypeConfiguration<CollaboratorAddress>
    {
        public void Configure(EntityTypeBuilder<CollaboratorAddress> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CollaboratorId)
                .IsRequired();

            builder.Property(c => c.PostalCode)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Number)
                .IsRequired();

            builder.Property(c => c.Neighborhood)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.City)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.FederatedState)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.Complement)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Created)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Updated);

            builder.Property(c => c.Active)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne<Collaborator>(x => x.Collaborator).WithMany(x => x.Addresses);

            builder.ToTable("collaborator_address", schema: "nog");
        }
    }
}