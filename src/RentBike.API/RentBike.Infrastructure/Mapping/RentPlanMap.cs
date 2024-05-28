using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBike.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class RentPlanMap : IEntityTypeConfiguration<RentPlan>
    {
        public void Configure(EntityTypeBuilder<RentPlan> builder)
        {
            builder.ToTable("rent_plans");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Days)
                .IsRequired()
                .HasColumnName("days")
                .HasColumnType("integer");
            builder.Property(p => p.CostPerDay)
                .IsRequired()
                .HasColumnName("cost_per_day")
                .HasColumnType("integer");
            builder.Property(p => p.FinePercentage)
                .IsRequired()
                .HasColumnName("fine_percentage")
                .HasColumnType("real");
            builder.HasData(new RentPlan 
            { 
                Days = 7, 
                CostPerDay = 30,
                FinePercentage = 20, 
                LastUpdated = null                
            });
            builder.HasData(new RentPlan
            {
                Days = 15,
                CostPerDay = 28,
                FinePercentage = 40,
                LastUpdated = null
            });
            builder.HasData(new RentPlan
            {
                Days = 30,
                CostPerDay = 22,
                FinePercentage = 60,
                LastUpdated = null
            });
        }
    }
}
