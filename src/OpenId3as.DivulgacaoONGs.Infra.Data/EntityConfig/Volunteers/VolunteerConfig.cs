using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Volunteers
{
    public class VolunteerConfig : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
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

            builder.HasMany<VolunteerAddress>(x => x.Addresses).WithOne(x => x.Volunteer);
            builder.HasMany<VolunteerPhone>(x => x.Phones).WithOne(x => x.Volunteer);

            builder.ToTable("volunteer", schema: "nog");
        }
    }
}
