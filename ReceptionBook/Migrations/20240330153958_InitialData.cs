using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReceptionBook.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"), "jana.mcleaf@gmail.com", "Jana", "McLeaf", null },
                    { new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"), "kane.miller@gmail.com", "Kane", "Miller", null }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Number", "Price", "Type" },
                values: new object[,]
                {
                    { new Guid("1ef4db57-145b-4b3d-903d-5486a621646e"), "101", 100m, "Single" },
                    { new Guid("56625ffa-ef46-461c-8867-2600d87a637a"), "104", 150m, "Double" },
                    { new Guid("7fac1f9c-b24b-4f3c-a4ea-4a0b3f5e2577"), "105", 100m, "Single" },
                    { new Guid("9fbbb0c1-a6bb-433c-9520-5a45592f0084"), "102", 150m, "Double" },
                    { new Guid("a48e654e-7e13-4a3a-83c8-18f179dd9eea"), "103", 100m, "Single" }
                });

            migrationBuilder.InsertData(
                table: "Maintenance",
                columns: new[] { "MaintenanceId", "Cost", "Description", "EndDate", "RoomId", "StartDate" },
                values: new object[,]
                {
                    { new Guid("b1bb6af1-f424-411f-9a30-34532015f75d"), 249.99m, "Broken window", null, new Guid("9fbbb0c1-a6bb-433c-9520-5a45592f0084"), new DateOnly(2024, 4, 1) },
                    { new Guid("bc1db9a1-8d53-46ab-93f4-e5a9b81e5119"), 100.50m, "Broken sink", new DateOnly(2024, 4, 8), new Guid("1ef4db57-145b-4b3d-903d-5486a621646e"), new DateOnly(2024, 4, 1) }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "ReservationId", "CustomerId", "EndDate", "RoomId", "StartDate", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { new Guid("0857ed5f-98fa-4fdd-b78f-daf6955588a3"), new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"), new DateOnly(2024, 4, 20), new Guid("56625ffa-ef46-461c-8867-2600d87a637a"), new DateOnly(2024, 4, 10), "Confirmed", 2100m },
                    { new Guid("856ebb5e-fa0e-48c7-8d4d-6605e304efaa"), new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"), new DateOnly(2024, 4, 7), new Guid("a48e654e-7e13-4a3a-83c8-18f179dd9eea"), new DateOnly(2024, 4, 5), "Pending", 200m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Maintenance",
                keyColumn: "MaintenanceId",
                keyValue: new Guid("b1bb6af1-f424-411f-9a30-34532015f75d"));

            migrationBuilder.DeleteData(
                table: "Maintenance",
                keyColumn: "MaintenanceId",
                keyValue: new Guid("bc1db9a1-8d53-46ab-93f4-e5a9b81e5119"));

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("0857ed5f-98fa-4fdd-b78f-daf6955588a3"));

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "ReservationId",
                keyValue: new Guid("856ebb5e-fa0e-48c7-8d4d-6605e304efaa"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: new Guid("7fac1f9c-b24b-4f3c-a4ea-4a0b3f5e2577"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("4b6693b4-f8bc-41b7-b7b8-4ef5b806335a"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: new Guid("1ef4db57-145b-4b3d-903d-5486a621646e"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: new Guid("56625ffa-ef46-461c-8867-2600d87a637a"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: new Guid("9fbbb0c1-a6bb-433c-9520-5a45592f0084"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: new Guid("a48e654e-7e13-4a3a-83c8-18f179dd9eea"));
        }
    }
}
