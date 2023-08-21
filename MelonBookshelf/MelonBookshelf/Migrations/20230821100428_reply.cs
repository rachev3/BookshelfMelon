using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelonBookshelf.Migrations
{
    public partial class reply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReplays");

            migrationBuilder.CreateTable(
                name: "CommentReplys",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceCommentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Reply = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReplys", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FK_CommentReplys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CommentReplys_ResourceComments_ResourceCommentId",
                        column: x => x.ResourceCommentId,
                        principalTable: "ResourceComments",
                        principalColumn: "CommentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplys_ResourceCommentId",
                table: "CommentReplys",
                column: "ResourceCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplys_UserId",
                table: "CommentReplys",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReplys");

            migrationBuilder.CreateTable(
                name: "CommentReplays",
                columns: table => new
                {
                    ReplayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceCommentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Replay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReplays", x => x.ReplayId);
                    table.ForeignKey(
                        name: "FK_CommentReplays_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CommentReplays_ResourceComments_ResourceCommentId",
                        column: x => x.ResourceCommentId,
                        principalTable: "ResourceComments",
                        principalColumn: "CommentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplays_ResourceCommentId",
                table: "CommentReplays",
                column: "ResourceCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplays_UserId",
                table: "CommentReplays",
                column: "UserId");
        }
    }
}
