using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumAppCore.Migrations
{
    public partial class addCommentChildrentIdNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentChildrentId",
                table: "Notifications",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentChildrentId",
                table: "Notifications");
        }
    }
}
