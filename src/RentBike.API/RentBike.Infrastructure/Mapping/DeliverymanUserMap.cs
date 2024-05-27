using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class DeliverymanUserMap : IEntityTypeConfiguration<DeliverymanUser>
    {
        public void Configure(EntityTypeBuilder<DeliverymanUser> builder) 
        {
            builder.ToTable("deliveryman_users");
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(100)");
            builder.Property(p => p.BirthDate)
                .IsRequired()
                .HasColumnName("birthdate")
                .HasColumnType("date");
            builder.Property(p => p.Cnpj)
                .IsRequired()
                .HasColumnName("cnpj")
                .HasColumnType("varchar(14)");
            builder.HasIndex(p => p.Cnpj).IsUnique();
            builder.HasOne(p => p.DriversLicense)
                .WithOne(p => p.DeliverymanUser)
                .HasForeignKey<DriversLicense>(d => d.DeliverymanId);
            builder.HasOne(p => p.DriversLicense)
                .WithOne(q => q.DeliverymanUser)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
