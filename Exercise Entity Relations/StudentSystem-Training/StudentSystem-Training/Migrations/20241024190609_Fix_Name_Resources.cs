using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentSystem_Training.Migrations
{
    public partial class Fix_Name_Resources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyProperty_Courses_CourseId",
                table: "MyProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty");

            migrationBuilder.RenameTable(
                name: "MyProperty",
                newName: "Resources");

            migrationBuilder.RenameIndex(
                name: "IX_MyProperty_CourseId",
                table: "Resources",
                newName: "IX_Resources_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Courses_CourseId",
                table: "Resources",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Courses_CourseId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.RenameTable(
                name: "Resources",
                newName: "MyProperty");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_CourseId",
                table: "MyProperty",
                newName: "IX_MyProperty_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyProperty_Courses_CourseId",
                table: "MyProperty",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
