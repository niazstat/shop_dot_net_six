using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddEmployeeExpensesHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmployeeExpenseHead",
                table: "ExpensesHeads",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployeeExpenses",
                table: "Expenses",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployeeExpenseHead",
                table: "ExpensesHeads");

            migrationBuilder.DropColumn(
                name: "IsEmployeeExpenses",
                table: "Expenses");
        }
    }
}
