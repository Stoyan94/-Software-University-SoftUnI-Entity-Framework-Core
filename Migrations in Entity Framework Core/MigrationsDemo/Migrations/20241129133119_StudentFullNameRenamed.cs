using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationsDemo.Migrations
{
    public partial class StudentFullNameRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema("SoftUni");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Students",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "FullName");
        }
    }
}
