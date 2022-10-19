using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendDev.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<int>(type: "int", nullable: false),
                    Tagline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Budget = table.Column<int>(type: "int", nullable: true),
                    Fees = table.Column<int>(type: "int", nullable: true),
                    AgeLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "MovieModelUserModel",
                columns: table => new
                {
                    UserMoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserMoviesId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieModelUserModel", x => new { x.UserMoviesId, x.UserMoviesId1 });
                    table.ForeignKey(
                        name: "FK_MovieModelUserModel_MovieModels_UserMoviesId",
                        column: x => x.UserMoviesId,
                        principalTable: "MovieModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieModelUserModel_Users_UserMoviesId1",
                        column: x => x.UserMoviesId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    isAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CreateDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewModels_MovieModels_MovieModelId",
                        column: x => x.MovieModelId,
                        principalTable: "MovieModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReviewModels_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreModelMovieModel_MovieGenresId1",
                table: "GenreModelMovieModel",
                column: "MovieGenresId1");

            migrationBuilder.CreateIndex(
                name: "IX_MovieModelUserModel_UserMoviesId1",
                table: "MovieModelUserModel",
                column: "UserMoviesId1");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModels_MovieModelId",
                table: "ReviewModels",
                column: "MovieModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModels_UserModelId",
                table: "ReviewModels",
                column: "UserModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreModelMovieModel");

            migrationBuilder.DropTable(
                name: "MovieModelUserModel");

            migrationBuilder.DropTable(
                name: "ReviewModels");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "MovieModels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
