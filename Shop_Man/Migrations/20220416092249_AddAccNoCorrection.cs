using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddAccNoCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccointNo",
                table: "BankAccounts",
                newName: "AccountNo");

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "BankAccounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "AccountNo",
                table: "BankAccounts",
                newName: "AccointNo");
        }
    }
}
