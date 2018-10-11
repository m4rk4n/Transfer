using Microsoft.EntityFrameworkCore.Migrations;

namespace Transfer.Migrations
{
    public partial class OptionalForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Vehicles_VehicleId",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Transfers_LastServiceTransferId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_LastServiceTransferId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Partners_VehicleId",
                table: "Partners");

            migrationBuilder.AlterColumn<int>(
                name: "LastServiceTransferId",
                table: "Vehicles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Partners",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LastServiceTransferId",
                table: "Vehicles",
                column: "LastServiceTransferId",
                unique: true,
                filter: "[LastServiceTransferId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Partners_VehicleId",
                table: "Partners",
                column: "VehicleId",
                unique: true,
                filter: "[VehicleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Vehicles_VehicleId",
                table: "Partners",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Transfers_LastServiceTransferId",
                table: "Vehicles",
                column: "LastServiceTransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Vehicles_VehicleId",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Transfers_LastServiceTransferId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_LastServiceTransferId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Partners_VehicleId",
                table: "Partners");

            migrationBuilder.AlterColumn<int>(
                name: "LastServiceTransferId",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Partners",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LastServiceTransferId",
                table: "Vehicles",
                column: "LastServiceTransferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_VehicleId",
                table: "Partners",
                column: "VehicleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Vehicles_VehicleId",
                table: "Partners",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Transfers_LastServiceTransferId",
                table: "Vehicles",
                column: "LastServiceTransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
