using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeTimeInReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_ArrivalDate",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ArrivalDate",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "DepartureTime",
                table: "Reservations",
                newName: "DepartureDateTime");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Reservations",
                newName: "ArrivalDateTime");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DepartureDate",
                table: "Reservations",
                newName: "IX_Reservations_ArrivalDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DepartureDateTime",
                table: "Reservations",
                column: "DepartureDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reservations_DepartureDateTime",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "DepartureDateTime",
                table: "Reservations",
                newName: "DepartureTime");

            migrationBuilder.RenameColumn(
                name: "ArrivalDateTime",
                table: "Reservations",
                newName: "DepartureDate");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_ArrivalDateTime",
                table: "Reservations",
                newName: "IX_Reservations_DepartureDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalDate",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ArrivalTime",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ArrivalDate",
                table: "Reservations",
                column: "ArrivalDate");
        }
    }
}
