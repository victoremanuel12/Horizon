using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class LoginMap : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.ToTable("Login");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
              .HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(l => l.Username)
                .HasColumnName("Username")
                .HasColumnType("varchar(36)")
                .IsRequired();

            builder.Property(l => l.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar(255)")
                .IsRequired();
        }
    }
}
