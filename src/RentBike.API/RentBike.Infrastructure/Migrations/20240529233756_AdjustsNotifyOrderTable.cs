using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustsNotifyOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("268e3099-2928-40c5-851e-86b74d76c09f"));

            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("dafdc559-eed7-461e-b158-18b672ef76ad"));

            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("db6cfcfd-bd18-4e57-aa87-9642d68e672d"));

            migrationBuilder.InsertData(
                table: "rent_plans",
                columns: new[] { "Id", "cost_per_day", "Created", "days", "fine_percentage", "LastUpdated" },
                values: new object[,]
                {
                    { new Guid("ddd9b382-e150-4f43-b821-6f96eaeb799a"), 22, new DateTime(2024, 5, 29, 23, 37, 56, 102, DateTimeKind.Utc).AddTicks(3831), 30, 60f, null },
                    { new Guid("de232093-fce8-4c2f-b5d3-d82d1ce5a3e9"), 30, new DateTime(2024, 5, 29, 23, 37, 56, 102, DateTimeKind.Utc).AddTicks(3810), 7, 20f, null },
                    { new Guid("f4e37029-4a31-48a5-a181-8e7a564ee19a"), 28, new DateTime(2024, 5, 29, 23, 37, 56, 102, DateTimeKind.Utc).AddTicks(3826), 15, 40f, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("ddd9b382-e150-4f43-b821-6f96eaeb799a"));

            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("de232093-fce8-4c2f-b5d3-d82d1ce5a3e9"));

            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("f4e37029-4a31-48a5-a181-8e7a564ee19a"));

            migrationBuilder.InsertData(
                table: "rent_plans",
                columns: new[] { "Id", "cost_per_day", "Created", "days", "fine_percentage", "LastUpdated" },
                values: new object[,]
                {
                    { new Guid("268e3099-2928-40c5-851e-86b74d76c09f"), 30, new DateTime(2024, 5, 29, 18, 5, 54, 59, DateTimeKind.Utc).AddTicks(124), 7, 20f, null },
                    { new Guid("dafdc559-eed7-461e-b158-18b672ef76ad"), 22, new DateTime(2024, 5, 29, 18, 5, 54, 59, DateTimeKind.Utc).AddTicks(147), 30, 60f, null },
                    { new Guid("db6cfcfd-bd18-4e57-aa87-9642d68e672d"), 28, new DateTime(2024, 5, 29, 18, 5, 54, 59, DateTimeKind.Utc).AddTicks(143), 15, 40f, null }
                });
        }
    }
}
