using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Infrastructure.Migrations
{
    public partial class SeedCinemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Sofia, The Mall", "Arena" },
                    { 2, "Sofia, Mega Mall", "Arena" },
                    { 3, "Varna, Grand Mall", "Arena" },
                    { 4, "Plovdiv, Markovo Tepe", "Arena" },
                    { 5, "Kardzhali, Arena", "Arena" },
                    { 6, "Smolyan, Arena", "Arena" },
                    { 7, "Sofia, Mall Sofia", "Cinema City" },
                    { 8, "Sofia, Paradise Center", "Cinema City" },
                    { 9, "Stara Zagora, Mall Galleria", "Cinema City" },
                    { 10, "Burgas, Mall Galeria", "Cinema City" },
                    { 11, "Plovdiv, Mall Plovdiv", "Cinema City" },
                    { 12, "Rousse, Mall Rousse", "Cinema City" },
                    { 13, "Sofia, Park Center", "Cine Grand" },
                    { 14, "Sofia, Ring Mall", "Cine Grand" },
                    { 15, "Sofia, Bulgaria Mall", "Cineland" },
                    { 16, "Pernik, Mall Pernik", "Cineland" },
                    { 17, "Veliko Tarnovo, Iskra", "Cineland" },
                    { 18, "Pleven, Central Mall", "Cineland" },
                    { 19, "Targovishte, Cinemagic", "Cineland" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: 19);
        }
    }
}
