using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBike.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class NotifyOrdersMap : IEntityTypeConfiguration<NotifyOrder>
    {
        public void Configure(EntityTypeBuilder<NotifyOrder> builder)
        {
            builder.ToTable("notify_orders");
            builder.Property(p => p.OrderId)
                .IsRequired()
                .HasColumnName("order_id")
                .HasColumnType("uuid");
            builder.Property(p => p.DeliverymanId)
                .IsRequired()
                .HasColumnName("deliveryman_id")
                .HasColumnType("uuid");
        }
    }
}
