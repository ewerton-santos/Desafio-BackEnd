using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatesRentAndRentPlanTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rent_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    days = table.Column<int>(type: "integer", nullable: false),
                    fine_percentage = table.Column<float>(type: "real", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rent_plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    deliveryman_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bike_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rentplan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    expected_end_date = table.Column<DateTime>(type: "date", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rent_bikes_bike_id",
                        column: x => x.bike_id,
                        principalTable: "bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rent_deliveryman_users_deliveryman_user_id",
                        column: x => x.deliveryman_user_id,
                        principalTable: "deliveryman_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rent_rent_plans_rentplan_id",
                        column: x => x.rentplan_id,
                        principalTable: "rent_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rent_bike_id",
                table: "rent",
                column: "bike_id");

            migrationBuilder.CreateIndex(
                name: "IX_rent_deliveryman_user_id",
                table: "rent",
                column: "deliveryman_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_rent_rentplan_id",
                table: "rent",
                column: "rentplan_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rent");

            migrationBuilder.DropTable(
                name: "rent_plans");
        }
    }
}
