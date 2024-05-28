using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToRentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "rents",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "rents");
        }
    }
}
