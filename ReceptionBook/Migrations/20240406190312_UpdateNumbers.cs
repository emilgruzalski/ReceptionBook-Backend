using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptionBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNumbers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "def60251-e923-4a88-ab68-cb05102280d1", "AQAAAAIAAYagAAAAEO8ByMz72FKArduFbMO6e9aKkIPGqffmBGkBx4agFQpoWf55ksvouIWz8hZzE6ktOw==", "a1643afe-c7f9-4269-b00e-9edafb61ea5c" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"),
                column: "PhoneNumber",
                value: "569519770");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("50aca617-2f75-4da7-a3af-074757f429fb"),
                column: "PhoneNumber",
                value: "726614409");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("eb36d4c7-3ed6-4a9b-8b61-dc42e1d40001"),
                column: "PhoneNumber",
                value: "753558744");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "820e2100-4194-4c29-add7-1062282ea2d0", "AQAAAAIAAYagAAAAEEd0nIa6oNHC8HQEccb0wQk4VUoVikIc88+lhdaVWvrJ6f8uQyJKfpk8aUYE/ST8bQ==", "c8d7d71e-5460-49e5-95b4-6efed1abc19c" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("109ba8ea-dbf1-4221-99dd-00052b252de2"),
                column: "PhoneNumber",
                value: "569-51-9770");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("50aca617-2f75-4da7-a3af-074757f429fb"),
                column: "PhoneNumber",
                value: "726-61-4409");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("eb36d4c7-3ed6-4a9b-8b61-dc42e1d40001"),
                column: "PhoneNumber",
                value: "753-55-8744");
        }
    }
}
