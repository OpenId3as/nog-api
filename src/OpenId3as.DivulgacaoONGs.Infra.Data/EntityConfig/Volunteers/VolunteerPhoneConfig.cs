using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Volunteers
{
    public class VolunteerPhoneConfig : IEntityTypeConfiguration<VolunteerPhone>
    {
        public void Configure(EntityTypeBuilder<VolunteerPhone> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.VolunteerId)
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

            builder.HasOne<Volunteer>(x => x.Volunteer).WithMany(x => x.Phones);

            builder.ToTable("volunteer_phone");
        }
    }
}
