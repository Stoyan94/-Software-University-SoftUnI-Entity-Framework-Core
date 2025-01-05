using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Infrastructure.Migrations
{
    public partial class SeedRest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, "In 1980s Hollywood, adult film star and aspiring actress Maxine Minx finally gets her big break. But as a mysterious killer stalks the starlets of Hollywood, a trail of blood threatens to reveal her sinister past.", 9, "MaXXXine" },
                    { 2, "Gru, Lucy, Margo, Edith, and Agnes welcome a new member to the family, Gru Jr., who is intent on tormenting his dad. Gru faces a new nemesis in Maxime Le Mal and his girlfriend Valentina, and the family is forced to go on the run.", 1, "Despicable Me 4" },
                    { 3, "After Garfield's unexpected reunion with his long-lost father, ragged alley cat Vic, he and his canine friend Odie are forced from their perfectly pampered lives to join Vic on a risky heist.", 1, "The Garfield Movie" },
                    { 4, "A new adaptation of the famous novel by Alexandre Dumas.", 4, "Le comte de Monte-Cristo" },
                    { 5, "Many years after the reign of Caesar, a young ape goes on a journey that will lead him to question everything he's been taught about the past and make choices that will define a future for apes and humans alike.", 2, "Kingdom of the Planet of the Apes" },
                    { 6, "This Summer, the world's favorite Bad Boys are back with their iconic mix of edge-of-your seat action and outrageous comedy but this time with a twist: Miami's finest are now on the run.", 2, "Bad Boys: Ride or Die" },
                    { 7, "The true story of Donna and Reverend WC Martin and their church in East Texas, in which 22 families adopted 77 children from the local foster system, igniting a movement for vulnerable children everywhere.", 4, "Sound of Hope: The Story of Possum Trot" },
                    { 8, "A young woman named Sam finds herself trapped in New York City during the early stages of an invasion by alien creatures with ultra-sensitive hearing.", 6, "A Quiet Place: Day One" },
                    { 9, "Marketing maven Kelly Jones wreaks havoc on launch director Cole Davis's already difficult task. When the White House deems the mission too important to fail, the countdown truly begins.", 10, "Fly Me to the Moon" }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "Id", "Factor", "Name" },
                values: new object[,]
                {
                    { 1, 1.0m, "Standard" },
                    { 2, 0.8m, "Student" },
                    { 3, 0.7m, "Senior" },
                    { 4, 0.5m, "Child" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Username" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "john_doe" },
                    { 2, "Jane", "Smith", "jane_smith" },
                    { 3, "Alice", "Johnson", "alice_johnson" },
                    { 4, "Bob", "Brown", "bob_brown" },
                    { 5, "Charlie", "Davis", "charlie_davis" },
                    { 6, "David", "Wilson", "david_wilson" },
                    { 7, "Emma", "Moore", "emma_moore" },
                    { 8, "Frank", "Clark", "frank_clark" },
                    { 9, "Grace", "Lee", "grace_lee" },
                    { 10, "Henry", "White", "henry_white" }
                });

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

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
