using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicaComEF.API.Data.Migrations
{
    public partial class AddFuncionalidadePlayList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayListModelId",
                table: "Musicas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PlayLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_PlayListModelId",
                table: "Musicas",
                column: "PlayListModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_PlayLists_PlayListModelId",
                table: "Musicas",
                column: "PlayListModelId",
                principalTable: "PlayLists",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_PlayLists_PlayListModelId",
                table: "Musicas");

            migrationBuilder.DropTable(
                name: "PlayLists");

            migrationBuilder.DropIndex(
                name: "IX_Musicas_PlayListModelId",
                table: "Musicas");

            migrationBuilder.DropColumn(
                name: "PlayListModelId",
                table: "Musicas");
        }
    }
}
