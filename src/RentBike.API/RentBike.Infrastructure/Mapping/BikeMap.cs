using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBike.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class BikeMap : IEntityTypeConfiguration<Bike>
    {
        public void Configure(EntityTypeBuilder<Bike> builder)
        {
            builder.ToTable("bikes");
            builder.Property(p => p.Plate)
                .HasMaxLength(7)
                .HasColumnName("plate")
                .HasColumnType("varchar(7)")
                .IsRequired();                
            builder.Property(p => p.Year)
                .HasColumnName("year")
                .HasColumnType("int")
                .IsRequired();
            builder.Property(p => p.Model)
                .HasMaxLength(50)
                .HasColumnName("model")
                .HasColumnType("varchar(50)")
                .IsRequired();
            builder.HasIndex(p => p.Plate).IsUnique();
        }
    }
}
