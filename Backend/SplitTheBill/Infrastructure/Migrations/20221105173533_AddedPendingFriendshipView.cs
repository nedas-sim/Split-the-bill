using Domain.Database;
using Domain.Views;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedPendingFriendshipView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = $@"
CREATE OR ALTER VIEW dbo.{nameof(PendingFriendshipView)} AS

SELECT
    uf.{nameof(UserFriendship.RequestSenderId)},
    uf.{nameof(UserFriendship.RequestReceiverId)},
    u_sender.{nameof(User.Email)} AS {nameof(PendingFriendshipView.SenderEmail)},
    u_sender.{nameof(User.Username)} AS {nameof(PendingFriendshipView.SenderUsername)},
    u_receiver.{nameof(User.Email)} AS {nameof(PendingFriendshipView.ReceiverEmail)},
    u_receiver.{nameof(User.Username)} AS {nameof(PendingFriendshipView.ReceiverUsername)},
    uf.{nameof(PendingFriendshipView.InvitedOn)}

FROM dbo.UserFriendships AS uf

INNER JOIN dbo.Users AS u_sender
    ON uf.{nameof(UserFriendship.RequestSenderId)} = u_sender.{nameof(User.Id)}

INNER JOIN dbo.Users AS u_receiver
    ON uf.{nameof(UserFriendship.RequestReceiverId)} = u_receiver.{nameof(User.Id)}

WHERE uf.{nameof(UserFriendship.AcceptedOn)} IS NULL";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = $"DROP VIEW dbo.{nameof(PendingFriendshipView)}";
            migrationBuilder.Sql(sql);
        }
    }
}
