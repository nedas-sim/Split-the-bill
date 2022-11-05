using Domain.Database;
using Domain.Views;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedAcceptedFriendshipView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = $@"
CREATE OR ALTER VIEW dbo.{nameof(AcceptedFriendshipView)} AS

SELECT
    uf.{nameof(UserFriendship.RequestSenderId)},
    uf.{nameof(UserFriendship.RequestReceiverId)},
    u_sender.{nameof(User.Email)} AS {nameof(AcceptedFriendshipView.SenderEmail)},
    u_sender.{nameof(User.Username)} AS {nameof(AcceptedFriendshipView.SenderUsername)},
    u_receiver.{nameof(User.Email)} AS {nameof(AcceptedFriendshipView.ReceiverEmail)},
    u_receiver.{nameof(User.Username)} AS {nameof(AcceptedFriendshipView.ReceiverUsername)},
    uf.{nameof(UserFriendship.AcceptedOn)}

FROM dbo.UserFriendships AS uf

INNER JOIN dbo.Users AS u_sender
    ON uf.{nameof(UserFriendship.RequestSenderId)} = u_sender.{nameof(User.Id)}

INNER JOIN dbo.Users AS u_receiver
    ON uf.{nameof(UserFriendship.RequestReceiverId)} = u_receiver.{nameof(User.Id)}

WHERE uf.{nameof(UserFriendship.AcceptedOn)} IS NOT NULL";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = $"DROP VIEW dbo.{nameof(AcceptedFriendshipView)}";
            migrationBuilder.Sql(sql);
        }
    }
}
