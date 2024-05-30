using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    is_available = table.Column<bool>(type: "boolean", nullable: false),
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
                name: "notify_orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deliveryman_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notify_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    delivery_fee = table.Column<float>(type: "real", nullable: false),
                    order_status = table.Column<int>(type: "int", nullable: false),
                    deliveryman_id = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rent_plans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    days = table.Column<int>(type: "integer", nullable: false),
                    cost_per_day = table.Column<int>(type: "integer", nullable: false),
                    fine_percentage = table.Column<float>(type: "real", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rent_plans", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "rents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    deliveryman_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    bike_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rentplan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "date", nullable: false),
                    end_date = table.Column<DateTime>(type: "date", nullable: true),
                    expected_end_date = table.Column<DateTime>(type: "date", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rents_bikes_bike_id",
                        column: x => x.bike_id,
                        principalTable: "bikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rents_deliveryman_users_deliveryman_user_id",
                        column: x => x.deliveryman_user_id,
                        principalTable: "deliveryman_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rents_rent_plans_rentplan_id",
                        column: x => x.rentplan_id,
                        principalTable: "rent_plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "rent_plans",
                columns: new[] { "Id", "cost_per_day", "Created", "days", "fine_percentage", "LastUpdated" },
                values: new object[,]
                {
                    { new Guid("1ef1ad4c-697c-4da7-8ddf-d8b971502d19"), 28, new DateTime(2024, 5, 30, 2, 56, 1, 833, DateTimeKind.Utc).AddTicks(7545), 15, 40f, null },
                    { new Guid("7ef69a50-73e8-4d2d-981a-95bf4844ba47"), 22, new DateTime(2024, 5, 30, 2, 56, 1, 833, DateTimeKind.Utc).AddTicks(7549), 30, 60f, null },
                    { new Guid("95b64a50-c449-4032-b90b-2281e549195f"), 30, new DateTime(2024, 5, 30, 2, 56, 1, 833, DateTimeKind.Utc).AddTicks(7529), 7, 20f, null }
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

            migrationBuilder.CreateIndex(
                name: "IX_rents_bike_id",
                table: "rents",
                column: "bike_id");

            migrationBuilder.CreateIndex(
                name: "IX_rents_deliveryman_user_id",
                table: "rents",
                column: "deliveryman_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_rents_rentplan_id",
                table: "rents",
                column: "rentplan_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_users");

            migrationBuilder.DropTable(
                name: "drivers_licenses");

            migrationBuilder.DropTable(
                name: "notify_orders");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "rents");

            migrationBuilder.DropTable(
                name: "bikes");

            migrationBuilder.DropTable(
                name: "deliveryman_users");

            migrationBuilder.DropTable(
                name: "rent_plans");
        }
    }
}
