using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCashPayCust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AutoSalarySheetNo",
                table: "SalarySheets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "CashPayments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeneratedSalarySheetNo",
                table: "SalarySheets",
                nullable: true,
                computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,PayDate))+'-'+CONVERT( VARCHAR,AutoSalarySheetNo)");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_CustomerID",
                table: "CashPayments",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CashPayments_Customer_CustomerID",
                table: "CashPayments",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashPayments_Customer_CustomerID",
                table: "CashPayments");

            migrationBuilder.DropIndex(
                name: "IX_CashPayments_CustomerID",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "AutoSalarySheetNo",
                table: "SalarySheets");

            migrationBuilder.DropColumn(
                name: "GeneratedSalarySheetNo",
                table: "SalarySheets");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "CashPayments");
        }
    }
}
