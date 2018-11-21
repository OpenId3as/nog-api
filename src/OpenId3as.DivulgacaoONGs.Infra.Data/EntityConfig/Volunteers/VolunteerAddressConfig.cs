using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenId3as.DivulgacaoONGs.Domain.Entities.Volunteers;
using System;

namespace OpenId3as.DivulgacaoONGs.Infra.Data.EntityConfig.Volunteers
{
    public class VolunteerAddressConfig : IEntityTypeConfiguration<VolunteerAddress>
    {
        public void Configure(EntityTypeBuilder<VolunteerAddress> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.VolunteerId)
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
                .HasDefaultValue(true);

            builder.HasOne<Volunteer>(x => x.Volunteer).WithMany(x => x.Addresses);

            builder.ToTable("volunteer_address");
        }
    }
}
