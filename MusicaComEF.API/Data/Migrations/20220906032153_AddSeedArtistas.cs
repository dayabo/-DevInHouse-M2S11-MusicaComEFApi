using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicaComEF.API.Data.Migrations
{
    public partial class AddSeedArtistas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Artistas",
                columns: new[] { "Id", "Idade", "Nome", "NomeArtistico", "PaisOrigem" },
                values: new object[] { 1, 49, "Daniel", "Daniel", "Brasil" });

            migrationBuilder.InsertData(
                table: "Artistas",
                columns: new[] { "Id", "Idade", "Nome", "NomeArtistico", "PaisOrigem" },
                values: new object[] { 2, 26, "Anitta", "Anitta", "Brasil" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Artistas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
