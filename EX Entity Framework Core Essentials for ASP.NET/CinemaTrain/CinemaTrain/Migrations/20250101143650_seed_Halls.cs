using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTrain.Migrations
{
    public partial class seed_Halls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "Id", "CinemaId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "IMAX Hall 1" },
                    { 2, 1, "3D Hall" },
                    { 3, 2, "VIP Hall" },
                    { 4, 2, "IMAX" },
                    { 5, 3, "3D Ultra VIP IMAX" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
