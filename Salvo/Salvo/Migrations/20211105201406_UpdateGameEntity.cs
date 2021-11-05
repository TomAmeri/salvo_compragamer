using Microsoft.EntityFrameworkCore.Migrations;

namespace Salvo.Migrations
{
    public partial class UpdateGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Games",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "joinDate",
                table: "GamePlayers",
                newName: "JoinDate");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "GamePlayers",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Games",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "GamePlayers",
                newName: "joinDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "GamePlayers",
                newName: "id");
        }
    }
}
