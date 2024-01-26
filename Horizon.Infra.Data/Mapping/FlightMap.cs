using Horizon.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class FlightMap : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flight");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property( a=> a.Canceled)
                .HasColumnName("Canceled")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(a => a.Code)
             .HasColumnName("Code")
             .HasColumnType("varchar(36)")
             .IsRequired();

            builder.Property(a => a.Time)
             .HasColumnName("Time")
             .HasColumnType("datetime")
             .IsRequired();

            builder.Property(a => a.OriginId)
             .HasColumnName("OriginId")
             .HasColumnType("varchar(36)")
             .IsRequired();

            builder.Property(a => a.DestinyId)
              .HasColumnName("DestinyId")
              .HasColumnType("varchar(36)")
              .IsRequired();

            builder.HasMany(a => a.Classes)
               .WithOne(c => c.Flight)  
               .HasForeignKey(c => c.FlightId)  
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
