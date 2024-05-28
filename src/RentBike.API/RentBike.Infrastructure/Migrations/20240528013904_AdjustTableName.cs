using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBike.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rent_bikes_bike_id",
                table: "rent");

            migrationBuilder.DropForeignKey(
                name: "FK_rent_deliveryman_users_deliveryman_user_id",
                table: "rent");

            migrationBuilder.DropForeignKey(
                name: "FK_rent_rent_plans_rentplan_id",
                table: "rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rent",
                table: "rent");

            migrationBuilder.RenameTable(
                name: "rent",
                newName: "rents");

            migrationBuilder.RenameIndex(
                name: "IX_rent_rentplan_id",
                table: "rents",
                newName: "IX_rents_rentplan_id");

            migrationBuilder.RenameIndex(
                name: "IX_rent_deliveryman_user_id",
                table: "rents",
                newName: "IX_rents_deliveryman_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_rent_bike_id",
                table: "rents",
                newName: "IX_rents_bike_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rents",
                table: "rents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rents_bikes_bike_id",
                table: "rents",
                column: "bike_id",
                principalTable: "bikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rents_deliveryman_users_deliveryman_user_id",
                table: "rents",
                column: "deliveryman_user_id",
                principalTable: "deliveryman_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rents_rent_plans_rentplan_id",
                table: "rents",
                column: "rentplan_id",
                principalTable: "rent_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rents_bikes_bike_id",
                table: "rents");

            migrationBuilder.DropForeignKey(
                name: "FK_rents_deliveryman_users_deliveryman_user_id",
                table: "rents");

            migrationBuilder.DropForeignKey(
                name: "FK_rents_rent_plans_rentplan_id",
                table: "rents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rents",
                table: "rents");

            migrationBuilder.RenameTable(
                name: "rents",
                newName: "rent");

            migrationBuilder.RenameIndex(
                name: "IX_rents_rentplan_id",
                table: "rent",
                newName: "IX_rent_rentplan_id");

            migrationBuilder.RenameIndex(
                name: "IX_rents_deliveryman_user_id",
                table: "rent",
                newName: "IX_rent_deliveryman_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_rents_bike_id",
                table: "rent",
                newName: "IX_rent_bike_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rent",
                table: "rent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rent_bikes_bike_id",
                table: "rent",
                column: "bike_id",
                principalTable: "bikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rent_deliveryman_users_deliveryman_user_id",
                table: "rent",
                column: "deliveryman_user_id",
                principalTable: "deliveryman_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rent_rent_plans_rentplan_id",
                table: "rent",
                column: "rentplan_id",
                principalTable: "rent_plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
