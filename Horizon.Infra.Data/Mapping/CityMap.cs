using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class CityMap : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
              .HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(a => a.Name)
             .HasColumnName("Name")
             .HasColumnType("varchar(50)")
             .IsRequired();

            builder.Property(a => a.UF)
             .HasColumnName("UF")
             .HasColumnType("varchar(2)")
             .IsRequired();

            builder.HasMany(c => c.Airports)
                .WithOne(a => a.City)
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
