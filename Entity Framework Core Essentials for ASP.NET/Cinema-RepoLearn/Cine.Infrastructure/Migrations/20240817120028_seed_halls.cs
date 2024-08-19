using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_RepoLearn.Infrastructure.Migrations
{
    public partial class seed_halls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "Id", "CinemaId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "IMAX Hall 1" },
                    { 2, 1, "IMAX-5D Hall 1" },
                    { 3, 1, "3D Hall 1" },
                    { 4, 2, "VIP Hall" },
                    { 5, 3, "IMAX Hall 1" }
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
