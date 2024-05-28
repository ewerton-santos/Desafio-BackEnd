using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingSomeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_available",
                table: "bikes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_available",
                table: "bikes");
        }
    }
}
