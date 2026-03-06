using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCompanyInransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "ChequeTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "CashReceive",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CashReceive",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "CashPayments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CashPayments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChequeTransactions_CompanyID",
                table: "ChequeTransactions",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceive_CompanyID",
                table: "CashReceive",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_CompanyID",
                table: "CashPayments",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_CashPayments_Company_CompanyID",
                table: "CashPayments",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CashReceive_Company_CompanyID",
                table: "CashReceive",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChequeTransactions_Company_CompanyID",
                table: "ChequeTransactions",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashPayments_Company_CompanyID",
                table: "CashPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_CashReceive_Company_CompanyID",
                table: "CashReceive");

            migrationBuilder.DropForeignKey(
                name: "FK_ChequeTransactions_Company_CompanyID",
                table: "ChequeTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ChequeTransactions_CompanyID",
                table: "ChequeTransactions");

            migrationBuilder.DropIndex(
                name: "IX_CashReceive_CompanyID",
                table: "CashReceive");

            migrationBuilder.DropIndex(
                name: "IX_CashPayments_CompanyID",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "ChequeTransactions");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "CashReceive");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "CashReceive");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "CashPayments");
        }
    }
}
