using Horizon.Domain.Domain;
using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class BuyMap : IEntityTypeConfiguration<Buy>
    {
        public void Configure(EntityTypeBuilder<Buy> builder)
        {
            builder.ToTable("Buy");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(a => a.BuyerId)
              .HasColumnName("BuyerId")
              .HasColumnType("varchar(36)")
              .IsRequired();

            builder.Property(a => a.VisitorId)
               .HasColumnName("VisitorId")
               .HasColumnType("varchar(36)")
               .IsRequired();

            builder.HasOne(a => a.Buyer)
               .WithMany(b => b.Buys)
               .HasForeignKey(a => a.BuyerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Visitor)
              .WithMany(b => b.Buys)
              .HasForeignKey(a => a.VisitorId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
