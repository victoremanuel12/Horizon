using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(a => a.ClassId)
                .HasColumnName("ClassId")
                .HasColumnType("varchar(36)")
                .IsRequired();

            builder.Property(a => a.BuyId)
                .HasColumnName("BuyId")
                .HasColumnType("varchar(36)")
                .IsRequired();

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

            builder.Property(a => a.Price)
                .HasColumnName("Price")
                .HasColumnType("decimal(17,2)")
                .IsRequired();

            builder.Property(a => a.Dispatch)
                .HasColumnName("Dispatch")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(a => a.Canceled)
                .HasColumnName("Canceled")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(a => a.BaggageId)
                .HasColumnName("BaggageId")
                .HasColumnType("varchar(36)");

            builder.HasOne(a => a.Class)
                .WithMany()  
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Buy)
                .WithMany(b => b.Tickets)
                .HasForeignKey(a => a.BuyId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
