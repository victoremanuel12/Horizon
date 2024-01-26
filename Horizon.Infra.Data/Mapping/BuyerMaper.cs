using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class BuyerMaper : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
            builder.ToTable("Buyer");

            builder.Property(a => a.Id)
              .HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(a => a.Name)
               .HasColumnName("Name")
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(a => a.Cpf)
              .HasColumnName("Cpf")
              .HasColumnType("varchar(11)")
              .IsRequired();

            builder.Property(a => a.Birthdate)
            .HasColumnName("Birthdate")
            .HasColumnType("date")
            .IsRequired();
        }
    }
}
