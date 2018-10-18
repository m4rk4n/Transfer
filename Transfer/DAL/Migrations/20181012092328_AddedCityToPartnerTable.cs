using Microsoft.EntityFrameworkCore.Migrations;

namespace Transfer.Migrations
{
    public partial class AddedCityToPartnerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Partners",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Partners");
        }
    }
}
