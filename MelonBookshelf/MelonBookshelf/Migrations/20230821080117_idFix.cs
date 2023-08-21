using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelonBookshelf.Migrations
{
    public partial class idFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id1",
                table: "WantedResources",
                newName: "WanterId");

            migrationBuilder.RenameColumn(
                name: "Id1",
                table: "ResourceComments",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "Id1",
                table: "Followers",
                newName: "FollowerId");

            migrationBuilder.RenameColumn(
                name: "Id1",
                table: "CommentReplays",
                newName: "ReplayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WanterId",
                table: "WantedResources",
                newName: "Id1");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "ResourceComments",
                newName: "Id1");

            migrationBuilder.RenameColumn(
                name: "FollowerId",
                table: "Followers",
                newName: "Id1");

            migrationBuilder.RenameColumn(
                name: "ReplayId",
                table: "CommentReplays",
                newName: "Id1");
        }
    }
}
