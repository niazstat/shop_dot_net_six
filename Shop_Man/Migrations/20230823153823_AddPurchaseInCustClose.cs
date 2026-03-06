using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddPurchaseInCustClose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseAmount",
                table: "YearCloseCustomerSumms",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseAmount",
                table: "YearCloseCustomerDetailses",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseAmount",
                table: "YearCloseCustomerSumms");

            migrationBuilder.DropColumn(
                name: "PurchaseAmount",
                table: "YearCloseCustomerDetailses");
        }
    }
}
