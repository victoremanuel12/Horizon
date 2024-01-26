using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class ClassMap : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Class");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(a => a.Seats)
               .HasColumnName("Seats")
               .HasColumnType("int")
               .IsRequired();
            builder.Property(a => a.Price)
               .HasColumnName("Price")
               .HasColumnType("decimal(17,2)")
               .IsRequired();

            builder.HasOne(a => a.ClassType)
                .WithMany()
                .HasForeignKey(a => a.ClassTypeId);

            builder.HasOne(a => a.Flight)
                .WithMany()
                .HasForeignKey(a => a.FlightId);

        }
    }
}
