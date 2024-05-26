using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class AdminUserMap : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {            
            builder.ToTable("admin_users");
            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(100)");
        }
    }
}
