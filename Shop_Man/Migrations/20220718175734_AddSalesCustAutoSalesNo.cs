using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSalesCustAutoSalesNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustwiseSalesNo",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuppwisePurNo",
                table: "PurchaseHeads",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustwiseSalesNo",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "SuppwisePurNo",
                table: "PurchaseHeads");
        }
    }
}
