using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedUserFriendshipTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFriendships",
                columns: table => new
                {
                    RequestSenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriendships", x => new { x.RequestSenderId, x.RequestReceiverId });
                    table.ForeignKey(
                        name: "FK_UserFriendships_Users_RequestReceiverId",
                        column: x => x.RequestReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFriendships_Users_RequestSenderId",
                        column: x => x.RequestSenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFriendships_RequestReceiverId",
                table: "UserFriendships",
                column: "RequestReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFriendships");
        }
    }
}
