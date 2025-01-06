using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Infrastructure.Migrations
{
    public partial class SeedHalls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Standard" },
                    { 2, "Kids" },
                    { 3, "Luxury" },
                    { 4, "3D" },
                    { 5, "IMAX/4DX" },
                    { 6, "Motion Controlled Seating" },
                    { 7, "Drive-In" }
                });

            migrationBuilder.InsertData(
                table: "CinemaHall",
                columns: new[] { "CinemaId", "HallId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 5, 1 },
                    { 5, 4 },
                    { 6, 1 },
                    { 6, 4 },
                    { 6, 5 },
                    { 7, 1 },
                    { 7, 4 },
                    { 7, 5 },
                    { 7, 7 },
                    { 8, 1 },
                    { 8, 3 },
                    { 8, 5 },
                    { 8, 6 },
                    { 9, 1 },
                    { 9, 4 },
                    { 9, 5 },
                    { 10, 1 },
                    { 10, 4 },
                    { 10, 5 },
                    { 11, 1 },
                    { 11, 4 },
                    { 11, 5 },
                    { 11, 6 },
                    { 12, 1 },
                    { 12, 4 }
                });

            migrationBuilder.InsertData(
                table: "CinemaHall",
                columns: new[] { "CinemaId", "HallId" },
                values: new object[,]
                {
                    { 12, 5 },
                    { 12, 6 },
                    { 13, 1 },
                    { 13, 4 },
                    { 13, 5 },
                    { 13, 6 },
                    { 14, 1 },
                    { 14, 4 },
                    { 14, 5 },
                    { 14, 6 },
                    { 15, 1 },
                    { 15, 4 },
                    { 15, 5 },
                    { 16, 1 },
                    { 16, 4 },
                    { 16, 5 },
                    { 17, 1 },
                    { 17, 4 },
                    { 17, 5 },
                    { 18, 1 },
                    { 18, 2 },
                    { 18, 4 },
                    { 18, 5 },
                    { 18, 7 },
                    { 19, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 6, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 7, 7 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 9, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 9, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 10, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 10, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 11, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 11, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 11, 6 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 12, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 12, 6 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 13, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 13, 6 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 14, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 14, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 14, 6 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 15, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 15, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 16, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 16, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 17, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 17, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 18, 2 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 18, 4 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 18, 5 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 18, 7 });

            migrationBuilder.DeleteData(
                table: "CinemaHall",
                keyColumns: new[] { "CinemaId", "HallId" },
                keyValues: new object[] { 19, 1 });

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

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
