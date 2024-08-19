using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_RepoLearn.Infrastructure.Migrations
{
    public partial class change_tariff_model_add_factor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Tariffs",
                newName: "Factor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Factor",
                table: "Tariffs",
                newName: "Price");
        }
    }
}
