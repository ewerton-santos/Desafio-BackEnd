﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentBikeUsers.Domain.Entities;

namespace RentBike.Infrastructure.Mapping
{
    public class DriversLicenseMap : IEntityTypeConfiguration<DriversLicence>
    {
        public void Configure(EntityTypeBuilder<DriversLicence> builder)
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
            builder.HasOne(p => p.DeliverymanUser)
                .WithOne(p => p.DriversLicence)
                .HasForeignKey<DriversLicence>(e => e.DeliverymanId)
                .IsRequired();
        }
    }
}