using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddEditUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHeads_Company_UserId",
                table: "PurchaseHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesHeads_Company_UserId",
                table: "SalesHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_Company_UserId",
                table: "SalesReturns");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHeads_Users_UserId",
                table: "PurchaseHeads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesHeads_Users_UserId",
                table: "SalesHeads",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_Users_UserId",
                table: "SalesReturns",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHeads_Users_UserId",
                table: "PurchaseHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesHeads_Users_UserId",
                table: "SalesHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesReturns_Users_UserId",
                table: "SalesReturns");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHeads_Company_UserId",
                table: "PurchaseHeads",
                column: "UserId",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesHeads_Company_UserId",
                table: "SalesHeads",
                column: "UserId",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesReturns_Company_UserId",
                table: "SalesReturns",
                column: "UserId",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
