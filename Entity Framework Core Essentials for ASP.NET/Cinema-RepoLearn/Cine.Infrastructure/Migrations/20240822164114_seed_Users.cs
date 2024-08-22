using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.Infrastructure.Migrations
{
    public partial class seed_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "UserName" },
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
