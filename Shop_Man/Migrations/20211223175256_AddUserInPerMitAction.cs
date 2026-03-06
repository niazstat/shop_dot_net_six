using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddUserInPerMitAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PermittedProjAction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PermittedProjAction_UserId",
                table: "PermittedProjAction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermittedProjAction_Users_UserId",
                table: "PermittedProjAction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermittedProjAction_Users_UserId",
                table: "PermittedProjAction");

            migrationBuilder.DropIndex(
                name: "IX_PermittedProjAction_UserId",
                table: "PermittedProjAction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PermittedProjAction");
        }
    }
}
