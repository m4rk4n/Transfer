using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Transfer.Migrations
{
    public partial class InitialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Partners",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgencyPartner",
                columns: table => new
                {
                    AgencyId = table.Column<int>(nullable: false),
                    PartnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyPartner", x => new { x.AgencyId, x.PartnerId });
                    table.ForeignKey(
                        name: "FK_AgencyPartner_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyPartner_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReservationTime = table.Column<DateTime>(nullable: false),
                    AgencyTravel = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    TransferFrom = table.Column<string>(nullable: true),
                    TransferTo = table.Column<string>(nullable: true),
                    TransferDuration = table.Column<string>(nullable: true),
                    PickUpAddress = table.Column<string>(nullable: true),
                    FlightNumber = table.Column<string>(nullable: true),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    PickUpDepartureTime = table.Column<DateTime>(nullable: false),
                    RoundTrip = table.Column<bool>(nullable: false),
                    NumberOfPassengers = table.Column<int>(nullable: false),
                    ChildSeats = table.Column<int>(nullable: false),
                    Boosters = table.Column<int>(nullable: false),
                    Notice = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    DoINeedSendMessageToClient = table.Column<bool>(nullable: false),
                    DidISendMessageToComment = table.Column<bool>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    PaypalEmail = table.Column<string>(nullable: true),
                    PaymentMethodNotice = table.Column<string>(nullable: true),
                    PriceInKn = table.Column<double>(nullable: false),
                    PriceInEur = table.Column<double>(nullable: false),
                    ConfirmTransfer = table.Column<bool>(nullable: false),
                    AgencyId = table.Column<int>(nullable: true),
                    PartnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfers_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    PlateNumber = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    LastServiceTransferId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Transfers_LastServiceTransferId",
                        column: x => x.LastServiceTransferId,
                        principalTable: "Transfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partners_VehicleId",
                table: "Partners",
                column: "VehicleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPartner_PartnerId",
                table: "AgencyPartner",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AgencyId",
                table: "Transfers",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_PartnerId",
                table: "Transfers",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LastServiceTransferId",
                table: "Vehicles",
                column: "LastServiceTransferId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Vehicles_VehicleId",
                table: "Partners",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Vehicles_VehicleId",
                table: "Partners");

            migrationBuilder.DropTable(
                name: "AgencyPartner");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropIndex(
                name: "IX_Partners_VehicleId",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Partners");
        }
    }
}
