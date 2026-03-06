using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddUserInExpensesHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesHeads_Company_UserId",
                table: "ExpensesHeads");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesHeads_Users_UserId",
                table: "ExpensesHeads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpensesHeads_Users_UserId",
                table: "ExpensesHeads");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpensesHeads_Company_UserId",
                table: "ExpensesHeads",
                column: "UserId",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
