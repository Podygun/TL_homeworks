using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBInitialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomAmentities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAmentities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PropertyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DailyPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    MinPersonCount = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPersonCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomTypes_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypeRoomAmentities",
                columns: table => new
                {
                    RoomAmentitiesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoomTypesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeRoomAmentities", x => new { x.RoomAmentitiesId, x.RoomTypesId });
                    table.ForeignKey(
                        name: "FK_RoomTypeRoomAmentities_RoomAmentities_RoomAmentitiesId",
                        column: x => x.RoomAmentitiesId,
                        principalTable: "RoomAmentities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomTypeRoomAmentities_RoomTypes_RoomTypesId",
                        column: x => x.RoomTypesId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypeRoomServices",
                columns: table => new
                {
                    RoomServicesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoomTypesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeRoomServices", x => new { x.RoomServicesId, x.RoomTypesId });
                    table.ForeignKey(
                        name: "FK_RoomTypeRoomServices_RoomServices_RoomServicesId",
                        column: x => x.RoomServicesId,
                        principalTable: "RoomServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomTypeRoomServices_RoomTypes_RoomTypesId",
                        column: x => x.RoomTypesId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeRoomAmentities_RoomTypesId",
                table: "RoomTypeRoomAmentities",
                column: "RoomTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeRoomServices_RoomTypesId",
                table: "RoomTypeRoomServices",
                column: "RoomTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_PropertyId",
                table: "RoomTypes",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomTypeRoomAmentities");

            migrationBuilder.DropTable(
                name: "RoomTypeRoomServices");

            migrationBuilder.DropTable(
                name: "RoomAmentities");

            migrationBuilder.DropTable(
                name: "RoomServices");

            migrationBuilder.DropTable(
                name: "RoomTypes");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
