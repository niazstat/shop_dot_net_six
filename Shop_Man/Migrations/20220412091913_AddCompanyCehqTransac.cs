using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCompanyCehqTransac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Banks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Banks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "BankAccounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_CompanyID",
                table: "Banks",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_CompanyID",
                table: "BankAccounts",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Company_CompanyID",
                table: "BankAccounts",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Company_CompanyID",
                table: "Banks",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Company_CompanyID",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Company_CompanyID",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_CompanyID",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_CompanyID",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "BankAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Banks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
