using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendDev.Migrations
{
    public partial class fixReviewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "ReviewModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateDateTime",
                table: "ReviewModels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
