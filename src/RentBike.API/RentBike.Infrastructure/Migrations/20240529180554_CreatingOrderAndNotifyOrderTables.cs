using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatingOrderAndNotifyOrderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "notify_orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    deliveryman_id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliverymanUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notify_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notify_orders_deliveryman_users_DeliverymanUserId",
                        column: x => x.DeliverymanUserId,
                        principalTable: "deliveryman_users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_notify_orders_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "rent_plans",
                columns: new[] { "Id", "cost_per_day", "Created", "days", "fine_percentage", "LastUpdated" },
                values: new object[,]
                {
                    { new Guid("268e3099-2928-40c5-851e-86b74d76c09f"), 30, new DateTime(2024, 5, 29, 18, 5, 54, 59, DateTimeKind.Utc).AddTicks(124), 7, 20f, null },
                    { new Guid("dafdc559-eed7-461e-b158-18b672ef76ad"), 22, new DateTime(2024, 5, 29, 18, 5, 54, 59, DateTimeKind.Utc).AddTicks(147), 30, 60f, null },
                    { new Guid("db6cfcfd-bd18-4e57-aa87-9642d68e672d"), 28, new DateTime(2024, 5, 29, 18, 5, 54, 59, DateTimeKind.Utc).AddTicks(143), 15, 40f, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_notify_orders_DeliverymanUserId",
                table: "notify_orders",
                column: "DeliverymanUserId");

            migrationBuilder.CreateIndex(
                name: "IX_notify_orders_order_id",
                table: "notify_orders",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notify_orders");

            migrationBuilder.DropTable(
                name: "orders");

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
                    { new Guid("a903d959-ab33-4b31-848c-964d381de75f"), 22, new DateTime(2024, 5, 28, 2, 33, 26, 628, DateTimeKind.Utc).AddTicks(3102), 30, 60f, null },
                    { new Guid("dc4ba287-eb02-4a4b-a22c-f12e78072963"), 30, new DateTime(2024, 5, 28, 2, 33, 26, 628, DateTimeKind.Utc).AddTicks(3076), 7, 20f, null },
                    { new Guid("e1137606-54d5-4ba0-b6d9-17cc9f34427e"), 28, new DateTime(2024, 5, 28, 2, 33, 26, 628, DateTimeKind.Utc).AddTicks(3097), 15, 40f, null }
                });
        }
    }
}
