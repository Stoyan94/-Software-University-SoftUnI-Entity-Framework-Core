using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogTraining.Migrations
{
    public partial class Author_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                schema: "blg",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "blg",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Authors_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "blg",
                        principalTable: "Blogs",
                        principalColumn: "BlogId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                schema: "blg",
                table: "Blogs",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BlogId",
                schema: "blg",
                table: "Authors",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                schema: "blg",
                table: "Blogs",
                column: "AuthorId",
                principalSchema: "blg",
                principalTable: "Authors",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                schema: "blg",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "blg");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_AuthorId",
                schema: "blg",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                schema: "blg",
                table: "Blogs");
        }
    }
}
