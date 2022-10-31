using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendDev.Migrations
{
    public partial class addUserToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewModels_Users_UserModelId",
                table: "ReviewModels");

            migrationBuilder.DropIndex(
                name: "IX_ReviewModels_UserModelId",
                table: "ReviewModels");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "ReviewModels");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ReviewModels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModels_UserId",
                table: "ReviewModels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewModels_Users_UserId",
                table: "ReviewModels",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewModels_Users_UserId",
                table: "ReviewModels");

            migrationBuilder.DropIndex(
                name: "IX_ReviewModels_UserId",
                table: "ReviewModels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ReviewModels");

            migrationBuilder.AddColumn<Guid>(
                name: "UserModelId",
                table: "ReviewModels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewModels_UserModelId",
                table: "ReviewModels",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewModels_Users_UserModelId",
                table: "ReviewModels",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
