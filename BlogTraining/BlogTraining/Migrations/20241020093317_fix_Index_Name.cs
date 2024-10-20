using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogTraining.Migrations
{
    public partial class fix_Index_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "ix_Blogs_Name_Unique",
                schema: "blg",
                table: "Blogs",
                newName: "IX_Blogs_Name_Unique");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_Blogs_Name_Unique",
                schema: "blg",
                table: "Blogs",
                newName: "ix_Blogs_Name_Unique");
        }
    }
}
