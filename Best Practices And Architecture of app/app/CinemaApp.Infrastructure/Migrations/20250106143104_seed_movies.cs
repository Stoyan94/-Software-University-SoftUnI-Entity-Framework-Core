using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApp.Infrastructure.Migrations
{
    public partial class seed_movies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Genre", "Title" },
                values: new object[,]
                {
                    { 1, "In 1980s Hollywood, adult film star and aspiring actress Maxine Minx finally gets her big break. But as a mysterious killer stalks the starlets of Hollywood, a trail of blood threatens to reveal her sinister past.", 9, "MaXXXine" },
                    { 2, "Gru, Lucy, Margo, Edith, and Agnes welcome a new member to the family, Gru Jr., who is intent on tormenting his dad. Gru faces a new nemesis in Maxime Le Mal and his girlfriend Valentina, and the family is forced to go on the run.", 1, "Despicable Me 4" },
                    { 3, "After Garfield's unexpected reunion with his long-lost father, ragged alley cat Vic, he and his canine friend Odie are forced from their perfectly pampered lives to join Vic on a risky heist.", 1, "The Garfield Movie" },
                    { 4, "A new adaptation of the famous novel by Alexandre Dumas.", 4, "Le comte de Monte-Cristo" },
                    { 5, "Many years after the reign of Caesar, a young ape goes on a journey that will lead him to question everything he's been taught about the past and make choices that will define a future for apes and humans alike.", 2, "Kingdom of the Planet of the Apes" },
                    { 6, "This Summer, the world's favorite Bad Boys are back with their iconic mix of edge-of-your seat action and outrageous comedy but this time with a twist: Miami's finest are now on the run.", 2, "Bad Boys: Ride or Die" },
                    { 7, "The true story of Donna and Reverend WC Martin and their church in East Texas, in which 22 families adopted 77 children from the local foster system, igniting a movement for vulnerable children everywhere.", 4, "Sound of Hope: The Story of Possum Trot" },
                    { 8, "A young woman named Sam finds herself trapped in New York City during the early stages of an invasion by alien creatures with ultra-sensitive hearing.", 6, "A Quiet Place: Day One" },
                    { 9, "Marketing maven Kelly Jones wreaks havoc on launch director Cole Davis's already difficult task. When the White House deems the mission too important to fail, the countdown truly begins.", 10, "Fly Me to the Moon" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
