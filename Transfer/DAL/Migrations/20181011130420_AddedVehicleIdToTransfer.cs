using Microsoft.EntityFrameworkCore.Migrations;

namespace Transfer.Migrations
{
    public partial class AddedVehicleIdToTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Transfers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Transfers");
        }
    }
}
