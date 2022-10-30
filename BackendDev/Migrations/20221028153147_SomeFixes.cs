using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendDev.Migrations
{
    public partial class SomeFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreModelMovieModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "GenreModelBdMovieModel",
                columns: table => new
                {
                    MovieGenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieGenresId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreModelBdMovieModel", x => new { x.MovieGenresId, x.MovieGenresId1 });
                    table.ForeignKey(
                        name: "FK_GenreModelBdMovieModel_Genres_MovieGenresId",
                        column: x => x.MovieGenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreModelBdMovieModel_MovieModels_MovieGenresId1",
                        column: x => x.MovieGenresId1,
                        principalTable: "MovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreModelBdMovieModel_MovieGenresId1",
                table: "GenreModelBdMovieModel",
                column: "MovieGenresId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreModelBdMovieModel");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GenreModelMovieModel",
                columns: table => new
                {
                    MovieGenresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieGenresId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreModelMovieModel", x => new { x.MovieGenresId, x.MovieGenresId1 });
                    table.ForeignKey(
                        name: "FK_GenreModelMovieModel_Genres_MovieGenresId",
                        column: x => x.MovieGenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreModelMovieModel_MovieModels_MovieGenresId1",
                        column: x => x.MovieGenresId1,
                        principalTable: "MovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreModelMovieModel_MovieGenresId1",
                table: "GenreModelMovieModel",
                column: "MovieGenresId1");
        }
    }
}
