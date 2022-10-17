using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendDev.Migrations
{
    public partial class addBasicDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "MovieModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    AgeLimit = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieModels_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieModelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_MovieModels_MovieModelId",
                        column: x => x.MovieModelId,
                        principalTable: "MovieModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReviewModels",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    isAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    CreateDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieModelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserModelId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                name: "IX_Genres_MovieModelId",
                table: "Genres",
                column: "MovieModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieModels_UserModelId",
                table: "MovieModels",
                column: "UserModelId");

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
                name: "Genres");

            migrationBuilder.DropTable(
                name: "ReviewModels");

            migrationBuilder.DropTable(
                name: "MovieModels");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
