using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Model = OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.Entities;

namespace OpenId3as.DivulgacaoONGs.Infra.CrossCutting.Log.EntityConfig
{
    public class LogConfig : IEntityTypeConfiguration<Model.Log>
    {
        public void Configure(EntityTypeBuilder<Model.Log> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.EventId);

            builder.Property(c => c.LogLevel)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Message)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(c => c.Created)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);

            builder.ToTable("log", schema: "log");
        }
    }
}
