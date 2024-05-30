﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RentBike.Infrastructure;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240529233756_AdjustsNotifyOrderTable")]
    partial class AdjustsNotifyOrderTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RentBike.Domain.Entities.Bike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean")
                        .HasColumnName("is_available");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("model");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)")
                        .HasColumnName("plate");

                    b.Property<int>("Year")
                        .HasColumnType("int")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.HasIndex("Plate")
                        .IsUnique();

                    b.ToTable("bikes", (string)null);
                });

            modelBuilder.Entity("RentBike.Domain.Entities.NotifyOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DeliverymanId")
                        .HasColumnType("uuid")
                        .HasColumnName("deliveryman_id");

                    b.Property<Guid?>("DeliverymanUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.HasKey("Id");

                    b.HasIndex("DeliverymanUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("notify_orders", (string)null);
                });

            modelBuilder.Entity("RentBike.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("DeliveryFee")
                        .HasColumnType("real")
                        .HasColumnName("delivery_fee");

                    b.Property<Guid?>("DeliverymanId")
                        .HasColumnType("uuid")
                        .HasColumnName("deliveryman_id");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int")
                        .HasColumnName("order_status");

                    b.HasKey("Id");

                    b.ToTable("orders", (string)null);
                });

            modelBuilder.Entity("RentBike.Domain.Entities.Rent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BikeId")
                        .HasColumnType("uuid")
                        .HasColumnName("bike_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DeliverymanUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("deliveryman_user_id");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<DateTime?>("ExpectedEndDate")
                        .HasColumnType("date")
                        .HasColumnName("expected_end_date");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RentPlanId")
                        .HasColumnType("uuid")
                        .HasColumnName("rentplan_id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("BikeId");

                    b.HasIndex("DeliverymanUserId");

                    b.HasIndex("RentPlanId");

                    b.ToTable("rents", (string)null);
                });

            modelBuilder.Entity("RentBike.Domain.Entities.RentPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CostPerDay")
                        .HasColumnType("integer")
                        .HasColumnName("cost_per_day");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Days")
                        .HasColumnType("integer")
                        .HasColumnName("days");

                    b.Property<float>("FinePercentage")
                        .HasColumnType("real")
                        .HasColumnName("fine_percentage");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("rent_plans", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("de232093-fce8-4c2f-b5d3-d82d1ce5a3e9"),
                            CostPerDay = 30,
                            Created = new DateTime(2024, 5, 29, 23, 37, 56, 102, DateTimeKind.Utc).AddTicks(3810),
                            Days = 7,
                            FinePercentage = 20f
                        },
                        new
                        {
                            Id = new Guid("f4e37029-4a31-48a5-a181-8e7a564ee19a"),
                            CostPerDay = 28,
                            Created = new DateTime(2024, 5, 29, 23, 37, 56, 102, DateTimeKind.Utc).AddTicks(3826),
                            Days = 15,
                            FinePercentage = 40f
                        },
                        new
                        {
                            Id = new Guid("ddd9b382-e150-4f43-b821-6f96eaeb799a"),
                            CostPerDay = 22,
                            Created = new DateTime(2024, 5, 29, 23, 37, 56, 102, DateTimeKind.Utc).AddTicks(3831),
                            Days = 30,
                            FinePercentage = 60f
                        });
                });

            modelBuilder.Entity("RentBikeUsers.Domain.Entities.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("admin_users", (string)null);
                });

            modelBuilder.Entity("RentBikeUsers.Domain.Entities.DeliverymanUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birthdate");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)")
                        .HasColumnName("cnpj");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.ToTable("deliveryman_users", (string)null);
                });

            modelBuilder.Entity("RentBikeUsers.Domain.Entities.DriversLicense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DeliverymanId")
                        .HasColumnType("uuid");

                    b.Property<int>("DriversLicenseType")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("image");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("number");

                    b.HasKey("Id");

                    b.HasIndex("DeliverymanId")
                        .IsUnique();

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("drivers_licenses", (string)null);
                });

            modelBuilder.Entity("RentBike.Domain.Entities.NotifyOrder", b =>
                {
                    b.HasOne("RentBikeUsers.Domain.Entities.DeliverymanUser", null)
                        .WithMany("NotifyOrders")
                        .HasForeignKey("DeliverymanUserId");

                    b.HasOne("RentBike.Domain.Entities.Order", null)
                        .WithMany("NotifyOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentBike.Domain.Entities.Rent", b =>
                {
                    b.HasOne("RentBike.Domain.Entities.Bike", "Bike")
                        .WithMany("Rents")
                        .HasForeignKey("BikeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentBikeUsers.Domain.Entities.DeliverymanUser", "DeliverymanUser")
                        .WithMany("Rents")
                        .HasForeignKey("DeliverymanUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentBike.Domain.Entities.RentPlan", "RentPlan")
                        .WithMany("Rents")
                        .HasForeignKey("RentPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bike");

                    b.Navigation("DeliverymanUser");

                    b.Navigation("RentPlan");
                });

            modelBuilder.Entity("RentBikeUsers.Domain.Entities.DriversLicense", b =>
                {
                    b.HasOne("RentBikeUsers.Domain.Entities.DeliverymanUser", "DeliverymanUser")
                        .WithOne("DriversLicense")
                        .HasForeignKey("RentBikeUsers.Domain.Entities.DriversLicense", "DeliverymanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliverymanUser");
                });

            modelBuilder.Entity("RentBike.Domain.Entities.Bike", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("RentBike.Domain.Entities.Order", b =>
                {
                    b.Navigation("NotifyOrders");
                });

            modelBuilder.Entity("RentBike.Domain.Entities.RentPlan", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("RentBikeUsers.Domain.Entities.DeliverymanUser", b =>
                {
                    b.Navigation("DriversLicense");

                    b.Navigation("NotifyOrders");

                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
