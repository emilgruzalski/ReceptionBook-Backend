using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptionBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "820e2100-4194-4c29-add7-1062282ea2d0", "EMIL.GRUZALSKI@GMAIL.COM", "AQAAAAIAAYagAAAAEEd0nIa6oNHC8HQEccb0wQk4VUoVikIc88+lhdaVWvrJ6f8uQyJKfpk8aUYE/ST8bQ==", "c8d7d71e-5460-49e5-95b4-6efed1abc19c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82e9fa50-d56f-4b75-8528-a361ed0fcc85", null, "AQAAAAIAAYagAAAAEAq2sArFZi/BwwEkX/aw47nD1PuysM/1yLFUCWcLV7QugQHiyosF7sxF57G1s6w3iA==", "242f668a-ae3b-4381-a27b-c1080a490caf" });
        }
    }
}
