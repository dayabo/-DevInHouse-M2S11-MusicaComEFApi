using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicaComEF.API.Data.Migrations
{
    public partial class AddPlayListNaMusica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayListMusicas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayListMusicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MusicaId = table.Column<int>(type: "int", nullable: false),
                    PlayListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayListMusicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayListMusicas_Musicas_MusicaId",
                        column: x => x.MusicaId,
                        principalTable: "Musicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayListMusicas_PlayLists_PlayListId",
                        column: x => x.PlayListId,
                        principalTable: "PlayLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayListMusicas_MusicaId",
                table: "PlayListMusicas",
                column: "MusicaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayListMusicas_PlayListId",
                table: "PlayListMusicas",
                column: "PlayListId");
        }
    }
}
