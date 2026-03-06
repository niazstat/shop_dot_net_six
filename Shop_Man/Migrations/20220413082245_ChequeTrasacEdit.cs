using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class ChequeTrasacEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "ChequeTransactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "ChequeTransactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Banks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChequeTransactions_CustomerID",
                table: "ChequeTransactions",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeTransactions_SupplierId",
                table: "ChequeTransactions",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChequeTransactions_Customer_CustomerID",
                table: "ChequeTransactions",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChequeTransactions_Suppliers_SupplierId",
                table: "ChequeTransactions",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChequeTransactions_Customer_CustomerID",
                table: "ChequeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChequeTransactions_Suppliers_SupplierId",
                table: "ChequeTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ChequeTransactions_CustomerID",
                table: "ChequeTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ChequeTransactions_SupplierId",
                table: "ChequeTransactions");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "ChequeTransactions");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "ChequeTransactions");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Banks");
        }
    }
}
