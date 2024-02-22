using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptionBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Maintenance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Maintenance",
                keyColumn: "MaintenanceId",
                keyValue: new Guid("b1bb6af1-f424-411f-9a30-34532015f75d"),
                column: "Cost",
                value: 250m);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("856ebb5e-fa0e-48c7-8d4d-6605e304efaa"),
                column: "CustomerId",
                value: new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"));

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Reservations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                table: "Maintenance",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Maintenance",
                keyColumn: "MaintenanceId",
                keyValue: new Guid("b1bb6af1-f424-411f-9a30-34532015f75d"),
                column: "Cost",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("856ebb5e-fa0e-48c7-8d4d-6605e304efaa"),
                column: "CustomerId",
                value: null);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Customers_CustomerId",
                table: "Reservations",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }
    }
}
