using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelonBookshelf.Migrations
{
    public partial class UserReplays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplays_AspNetUsers_Id",
                table: "CommentReplays");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CommentReplays",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplays_AspNetUsers_Id",
                table: "CommentReplays",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplays_AspNetUsers_Id",
                table: "CommentReplays");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CommentReplays",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplays_AspNetUsers_Id",
                table: "CommentReplays",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
