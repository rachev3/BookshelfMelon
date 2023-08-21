using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MelonBookshelf.Migrations
{
    public partial class commentsReplays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentReplays",
                columns: table => new
                {
                    Id1 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceCommentId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Replay = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentReplays", x => x.Id1);
                    table.ForeignKey(
                        name: "FK_CommentReplays_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentReplays_ResourceComments_ResourceCommentId",
                        column: x => x.ResourceCommentId,
                        principalTable: "ResourceComments",
                        principalColumn: "Id1");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplays_Id",
                table: "CommentReplays",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplays_ResourceCommentId",
                table: "CommentReplays",
                column: "ResourceCommentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentReplays");
        }
    }
}
