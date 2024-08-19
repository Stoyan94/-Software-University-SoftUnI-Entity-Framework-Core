using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema_RepoLearn.Infrastructure.Migrations
{
    public partial class fix_tariff_softUniDisc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 4,
                column: "Factor",
                value: 0.5m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 4,
                column: "Factor",
                value: 0.7m);
        }
    }
}
