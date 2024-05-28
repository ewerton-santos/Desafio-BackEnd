using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingColumnsAndChargeRentPlanData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cost_per_day",
                table: "rent_plans",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost_per_day",
                table: "rent_plans");
        }
    }
}
