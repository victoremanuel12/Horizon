using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class VisitorMap : IEntityTypeConfiguration<Visitor>
    {
        public void Configure(EntityTypeBuilder<Visitor> builder)
        {
            builder.ToTable("Visitor");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasConversion(prop => prop.ToString(), prop => new Guid(prop));

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

            builder.Property(a => a.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.HasMany(v => v.Buys)
                .WithOne(b => b.Visitor)
                .HasForeignKey(b => b.VisitorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
