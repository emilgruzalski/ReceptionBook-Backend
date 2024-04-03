using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReceptionBook.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "82e9fa50-d56f-4b75-8528-a361ed0fcc85", "emil.gruzalski@gmail.com", "Emil", "Grużalski", "EMIL.GRUZALSKI@GMAIL.COM", "AQAAAAIAAYagAAAAEAq2sArFZi/BwwEkX/aw47nD1PuysM/1yLFUCWcLV7QugQHiyosF7sxF57G1s6w3iA==", "242f668a-ae3b-4381-a27b-c1080a490caf", "emil.gruzalski@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b778396b-8016-492a-8f38-4188eaca1e1e",
                columns: new[] { "ConcurrencyStamp", "Email", "FirstName", "LastName", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "7010f22f-e7d2-43d2-95cf-64a37aa04cc0", "admin@receptionbook.com", "Admin", "Admin", "ADMIN", "AQAAAAIAAYagAAAAEBshXGpuwT9aVlnuaW+Dph/9asPcVAs5aJXb6A5BtBgmf4mJ6GTh6yv4dD2fkGWFwA==", "41e854a6-4194-403f-a14d-28310be11ed0", "admin" });
        }
    }
}
