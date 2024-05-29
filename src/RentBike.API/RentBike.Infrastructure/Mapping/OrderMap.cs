using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBike.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");
            builder.Property(p => p.DeliveryFee)
                .IsRequired()
                .HasColumnName("delivery_fee")
                .HasColumnType("real");
            builder.Property(p => p.OrderStatus)
                .IsRequired()
                .HasColumnName("order_status")
                .HasColumnType("int");
            builder.Property(p => p.DeliverymanId)
                .HasColumnName("deliveryman_id")
                .HasColumnType("uuid");            
        }
    }
}
