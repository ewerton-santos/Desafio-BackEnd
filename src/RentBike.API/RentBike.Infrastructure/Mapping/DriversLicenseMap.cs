using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class DriversLicenseMap : IEntityTypeConfiguration<DriversLicense>
    {
        public void Configure(EntityTypeBuilder<DriversLicense> builder)
        {
            builder.ToTable("drivers_licenses");
            builder.Property(p => p.Number)
                .IsRequired()
                .HasColumnName("number")
                .HasColumnType("varchar(10)");
            builder.Property(p => p.Image)
                .HasColumnName("image")
                .HasColumnType("varchar(200)");
            builder.HasIndex(p => p.Number).IsUnique();
        }
    }
}
