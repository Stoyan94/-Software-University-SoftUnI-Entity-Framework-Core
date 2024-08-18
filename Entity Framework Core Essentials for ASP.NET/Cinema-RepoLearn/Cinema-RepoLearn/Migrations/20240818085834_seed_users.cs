using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_RepoLearn.Migrations
{
    public partial class seed_users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName", "UserName" },
                values: new object[,]
                {
                    { 1, "Pesho", "Petrov", "Pesho123" },
                    { 2, "Pesho", "Ivanov", "Pesho789" },
                    { 3, "Ivan", "Ivanov", "vankata89" },
                    { 4, "Maria", "Petrova", "maria12312" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
