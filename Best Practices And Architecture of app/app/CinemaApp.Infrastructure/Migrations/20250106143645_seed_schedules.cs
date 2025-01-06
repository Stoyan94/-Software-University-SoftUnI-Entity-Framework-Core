using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Infrastructure.Migrations
{
    public partial class seed_schedules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "CinemaId", "Duration", "HallId", "MovieId", "Start" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 2, 0, 0, 0), 1, 1, new DateTime(2024, 7, 20, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new TimeSpan(0, 2, 0, 0, 0), 1, 1, new DateTime(2024, 7, 20, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new TimeSpan(0, 1, 45, 0, 0), 1, 2, new DateTime(2024, 7, 20, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5, new TimeSpan(0, 1, 30, 0, 0), 1, 2, new DateTime(2024, 7, 21, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 6, new TimeSpan(0, 2, 15, 0, 0), 5, 3, new DateTime(2024, 7, 22, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 11, new TimeSpan(0, 2, 0, 0, 0), 6, 3, new DateTime(2024, 7, 23, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 15, new TimeSpan(0, 1, 50, 0, 0), 4, 3, new DateTime(2024, 7, 24, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 19, new TimeSpan(0, 2, 10, 0, 0), 1, 1, new DateTime(2024, 7, 25, 21, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 17, new TimeSpan(0, 1, 45, 0, 0), 4, 9, new DateTime(2024, 7, 26, 22, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 18, new TimeSpan(0, 2, 5, 0, 0), 7, 8, new DateTime(2024, 7, 27, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 13, new TimeSpan(0, 1, 45, 0, 0), 6, 6, new DateTime(2024, 7, 28, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 10, new TimeSpan(0, 2, 0, 0, 0), 5, 5, new DateTime(2024, 7, 29, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 7, new TimeSpan(0, 1, 45, 0, 0), 7, 7, new DateTime(2024, 7, 30, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 4, new TimeSpan(0, 2, 0, 0, 0), 4, 4, new DateTime(2024, 7, 31, 18, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
