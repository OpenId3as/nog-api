using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Collaborators;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Collaborartors
{
    public class CollaboratorPhoneConfig : IEntityTypeConfiguration<CollaboratorPhone>
    {
        public void Configure(EntityTypeBuilder<CollaboratorPhone> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CollaboratorId)
                .IsRequired();

            builder.Property(c => c.Type)
                .IsRequired();

            builder.Property(c => c.Number)
                .IsRequired();

            builder.Property(c => c.Created)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Updated);

            builder.Property(c => c.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne<Collaborator>(x => x.Collaborator).WithMany(x => x.Phones);

            builder.ToTable("collaborator_phone", schema: "nog");
        }
    }
}
