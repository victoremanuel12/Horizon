using Horizon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Horizon.Infra.Data.Mapping
{
    internal class ClassTypeMap : IEntityTypeConfiguration<ClassType>
    {
        public void Configure(EntityTypeBuilder<ClassType> builder)
        {
            builder.ToTable("ClassType");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(a => a.Name)
              .HasColumnName("Name")
              .HasColumnType("varchar(20)")
              .IsRequired();

            builder.HasMany(a => a.Classes)
                .WithOne(c => c.ClassType)
                .HasForeignKey(c => c.ClassTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
