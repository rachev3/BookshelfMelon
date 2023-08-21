using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelonBookshelf.Migrations
{
    public partial class userIdComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplays_AspNetUsers_Id",
                table: "CommentReplays");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceComments_AspNetUsers_Id",
                table: "ResourceComments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ResourceComments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceComments_Id",
                table: "ResourceComments",
                newName: "IX_ResourceComments_UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CommentReplays",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReplays_Id",
                table: "CommentReplays",
                newName: "IX_CommentReplays_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplays_AspNetUsers_UserId",
                table: "CommentReplays",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceComments_AspNetUsers_UserId",
                table: "ResourceComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplays_AspNetUsers_UserId",
                table: "CommentReplays");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceComments_AspNetUsers_UserId",
                table: "ResourceComments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ResourceComments",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ResourceComments_UserId",
                table: "ResourceComments",
                newName: "IX_ResourceComments_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CommentReplays",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CommentReplays_UserId",
                table: "CommentReplays",
                newName: "IX_CommentReplays_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplays_AspNetUsers_Id",
                table: "CommentReplays",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceComments_AspNetUsers_Id",
                table: "ResourceComments",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
