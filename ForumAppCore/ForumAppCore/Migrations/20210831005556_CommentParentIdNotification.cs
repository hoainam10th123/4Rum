using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ForumAppCore.Migrations
{
    public partial class CommentParentIdNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CommentParentId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentParentId",
                table: "Notifications");
        }
    }
}
