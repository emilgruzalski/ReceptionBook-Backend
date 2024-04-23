using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptionBook.Migrations
{
    /// <inheritdoc />
    public partial class StatusUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c563aa9-e2b7-498e-95b9-a08ff37960ea", "AQAAAAIAAYagAAAAEBB35tdlkfSYDFSjG5hyShc66YRU3X1dCBcbwOpzgM2kqVSwUW1+auK0jYz5j5eydg==", "07485c2d-7ef3-42e6-89de-c83d4585a285" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("0857ed5f-98fa-4fdd-b78f-daf6955588a3"),
                columns: new[] { "EndDate", "StartDate", "Status" },
                values: new object[] { new DateOnly(2024, 4, 30), new DateOnly(2024, 4, 25), "Confirmed" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("856ebb5e-fa0e-48c7-8d4d-6605e304efaa"),
                columns: new[] { "EndDate", "StartDate", "Status" },
                values: new object[] { new DateOnly(2024, 4, 27), new DateOnly(2024, 4, 25), "Cancelled" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("e2afccbc-24df-4552-97dc-6af2b985d01a"),
                columns: new[] { "EndDate", "StartDate", "Status" },
                values: new object[] { new DateOnly(2024, 5, 6), new DateOnly(2024, 4, 30), "Confirmed" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reservations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "def60251-e923-4a88-ab68-cb05102280d1", "AQAAAAIAAYagAAAAEO8ByMz72FKArduFbMO6e9aKkIPGqffmBGkBx4agFQpoWf55ksvouIWz8hZzE6ktOw==", "a1643afe-c7f9-4269-b00e-9edafb61ea5c" });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("0857ed5f-98fa-4fdd-b78f-daf6955588a3"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2024, 4, 15), new DateOnly(2024, 4, 10) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("856ebb5e-fa0e-48c7-8d4d-6605e304efaa"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2024, 4, 7), new DateOnly(2024, 4, 5) });

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("e2afccbc-24df-4552-97dc-6af2b985d01a"),
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateOnly(2024, 4, 11), new DateOnly(2024, 4, 6) });
        }
    }
}
