using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_users", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "deliveryman_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    cnpj = table.Column<string>(type: "varchar(14)", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveryman_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "drivers_licenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<string>(type: "varchar(10)", nullable: false),
                    image = table.Column<string>(type: "varchar(200)", nullable: true),
                    DriversLicenseType = table.Column<int>(type: "integer", nullable: false),
                    DeliverymanId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers_licenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_drivers_licenses_deliveryman_users_DeliverymanId",
                        column: x => x.DeliverymanId,
                        principalTable: "deliveryman_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bikes_plate",
                table: "bikes",
                column: "plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deliveryman_users_cnpj",
                table: "deliveryman_users",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_drivers_licenses_DeliverymanId",
                table: "drivers_licenses",
                column: "DeliverymanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_drivers_licenses_number",
                table: "drivers_licenses",
                column: "number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_users");

            migrationBuilder.DropTable(
                name: "bikes");

            migrationBuilder.DropTable(
                name: "drivers_licenses");

            migrationBuilder.DropTable(
                name: "deliveryman_users");
        }
    }
}
