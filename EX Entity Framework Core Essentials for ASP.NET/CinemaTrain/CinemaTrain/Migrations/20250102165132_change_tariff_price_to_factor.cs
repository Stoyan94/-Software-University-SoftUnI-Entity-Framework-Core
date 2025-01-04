using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTrain.Migrations
{
    public partial class change_tariff_price_to_factor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Tariffs");

            migrationBuilder.AddColumn<decimal>(
                name: "Factor",
                table: "Tariffs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Factor",
                table: "Tariffs");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Tariffs",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
