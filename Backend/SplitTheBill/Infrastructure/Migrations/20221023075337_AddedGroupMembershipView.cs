using Domain.Database;
using Domain.Views;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

public partial class AddedGroupMembershipView : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        string sql = $@"
CREATE OR ALTER VIEW dbo.{nameof(GroupMembershipView)} AS

SELECT 
    ug.{nameof(UserGroup.UserId)}, 
    ug.{nameof(UserGroup.GroupId)},
    u.{nameof(User.Email)}, 
    g.{nameof(Group.Name)} AS {nameof(GroupMembershipView.GroupName)}, 
    ug.{nameof(UserGroup.AcceptedOn)} 

FROM dbo.UserGroups AS ug

INNER JOIN dbo.Users u
	ON ug.{nameof(UserGroup.UserId)} = u.{nameof(User.Id)}

INNER JOIN dbo.Groups g
	ON ug.{nameof(UserGroup.GroupId)} = g.{nameof(Group.Id)}

WHERE ug.{nameof(UserGroup.AcceptedOn)} IS NOT NULL";

        migrationBuilder.Sql(sql);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        string sql = $"DROP VIEW dbo.{nameof(GroupMembershipView)}";
        migrationBuilder.Sql(sql);
    }
}
