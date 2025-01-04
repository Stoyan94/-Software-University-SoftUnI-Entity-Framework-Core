using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTrain.Migrations
{
    public partial class change_tariff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_Movies_MovieId",
                table: "Tariffs");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Tariffs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_Movies_MovieId",
                table: "Tariffs",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tariffs_Movies_MovieId",
                table: "Tariffs");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Tariffs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tariffs_Movies_MovieId",
                table: "Tariffs",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
