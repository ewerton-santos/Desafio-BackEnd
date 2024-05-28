using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChargeRentPlanData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "rent_plans",
                columns: new[] { "Id", "cost_per_day", "Created", "days", "fine_percentage", "LastUpdated" },
                values: new object[,]
                {
                    { new Guid("a903d959-ab33-4b31-848c-964d381de75f"), 22, new DateTime(2024, 5, 28, 2, 33, 26, 628, DateTimeKind.Utc).AddTicks(3102), 30, 60f, null },
                    { new Guid("dc4ba287-eb02-4a4b-a22c-f12e78072963"), 30, new DateTime(2024, 5, 28, 2, 33, 26, 628, DateTimeKind.Utc).AddTicks(3076), 7, 20f, null },
                    { new Guid("e1137606-54d5-4ba0-b6d9-17cc9f34427e"), 28, new DateTime(2024, 5, 28, 2, 33, 26, 628, DateTimeKind.Utc).AddTicks(3097), 15, 40f, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("a903d959-ab33-4b31-848c-964d381de75f"));

            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("dc4ba287-eb02-4a4b-a22c-f12e78072963"));

            migrationBuilder.DeleteData(
                table: "rent_plans",
                keyColumn: "Id",
                keyValue: new Guid("e1137606-54d5-4ba0-b6d9-17cc9f34427e"));
        }
    }
}
