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
            builder.Property(p => p.FinePercentage)
                .IsRequired()
                .HasColumnName("fine_percentage")
                .HasColumnType("real");
        }
    }
}
