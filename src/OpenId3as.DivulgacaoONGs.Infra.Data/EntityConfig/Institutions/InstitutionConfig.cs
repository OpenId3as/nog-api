using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Institutions
{
    public class InstitutionConfig : IEntityTypeConfiguration<Institution>
    {
        public void Configure(EntityTypeBuilder<Institution> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.LegalName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.TradeName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Foundation)
                .IsRequired();

            builder.Property(c => c.Created)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Updated);

            builder.Property(c => c.Active)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany<InstitutionAddress>(x => x.Addresses).WithOne(x => x.Institution);
            builder.HasMany<InstitutionPhone>(x => x.Phones).WithOne(x => x.Institution);

            builder.ToTable("institution");
        }
    }
}
