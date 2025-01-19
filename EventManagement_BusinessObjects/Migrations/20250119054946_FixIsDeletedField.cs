using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement_BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class FixIsDeletedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41e59d70-cad0-4f24-83d8-c93913263b2c", "admin@32mine.net", "AQAAAAIAAYagAAAAEMDA6mq6jkvONN8fvzSgSFzEQMLQnKK9GRrEZ6TPgS2e9UzzFOyY7dMpmf8SmqKH0w==", "5d3a6d60-541e-4031-951e-4624eb07ec12" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "094b7961-7dd7-4f26-bdfa-cf5147e55137", null, "AQAAAAIAAYagAAAAECKCp10VXL7LYrawTKGklni7MYDafJOZhsYC/W5EXorXMLsCXGDotYiQlclcF8Z45A==", "304977df-b340-4005-9e44-29644b3f6ed2" });
        }
    }
}
