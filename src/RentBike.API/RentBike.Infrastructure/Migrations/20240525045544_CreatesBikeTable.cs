using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatesBikeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    plate = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false),
                    model = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bikes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bikes_plate",
                table: "bikes",
                column: "plate",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bikes");
        }
    }
}
