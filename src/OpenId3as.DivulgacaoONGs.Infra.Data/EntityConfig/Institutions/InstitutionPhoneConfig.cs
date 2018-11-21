using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Institutions;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Institutions
{
    public class InstitutionPhoneConfig : IEntityTypeConfiguration<InstitutionPhone>
    {
        public void Configure(EntityTypeBuilder<InstitutionPhone> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.InstitutionId)
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

            builder.HasOne<Institution>(x => x.Institution).WithMany(x => x.Phones); ;

            builder.ToTable("institution_phone");
        }
    }
}
