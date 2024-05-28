using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBike.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class RentMap : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.ToTable("rents");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.DeliverymanUserId)
                .IsRequired()
                .HasColumnName("deliveryman_user_id")
                .HasColumnType("uuid");
            builder.Property(p => p.BikeId)
                .IsRequired()
                .HasColumnName("bike_id")
                .HasColumnType("uuid");
            builder.Property(p => p.RentPlanId)
                .IsRequired()
                .HasColumnName("rentplan_id")
                .HasColumnType("uuid");
            builder.Property(p => p.StartDate)
                .IsRequired()
                .HasColumnName("start_date")
                .HasColumnType("date");
            builder.Property(p => p.EndDate)
                .HasColumnName("end_date")
                .HasColumnType("date");
            builder.Property(p => p.ExpectedEndDate)
                .HasColumnName("expected_end_date")
                .HasColumnType("date");
            builder.Property(p => p.IsActive)
                .HasColumnName("is_active")
                .HasColumnType("boolean")
                .HasConversion<bool>();
            builder.HasOne(p => p.DeliverymanUser)
                .WithMany(p => p.Rents)
                .HasForeignKey(d => d.DeliverymanUserId);
            builder.HasOne(p => p.Bike)
                .WithMany(p => p.Rents)
                .HasForeignKey(d => d.BikeId);
            builder.HasOne(p => p.RentPlan)
                .WithMany(p => p.Rents)
                .HasForeignKey(d => d.RentPlanId);
        }
    }
}
