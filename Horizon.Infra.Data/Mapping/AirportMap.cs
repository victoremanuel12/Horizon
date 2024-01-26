using Azure;
using Horizon.Domain.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Horizon.Infra.Data.Mapping
{
    internal class AirportMap : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> builder)
        {
            builder.ToTable("Airport");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasConversion(prop => prop.ToString(), prop => new Guid(prop));

            builder.Property(a => a.CityId)
                .HasColumnName("CityId")
                .HasColumnType("varchar(36)")
                .IsRequired();

            builder.Property(a => a.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(a => a.Code)
                .HasColumnName("Code")
                .HasColumnType("varchar(4)")
                .IsRequired();

            builder.HasOne(a => a.City)
                .WithMany()
                .HasForeignKey(a => a.CityId);
            
            //.WithMany(): esta parte especifica que o relacionamento é um relacionamento muitos para um. Isso significa que muitos aeroportos podem estar associados
            //    a uma cidade, mas cada aeroporto está associado a apenas uma cidade.A ausência de um argumento WithMany()implica um relacionamento um-para
        }
    }
}
