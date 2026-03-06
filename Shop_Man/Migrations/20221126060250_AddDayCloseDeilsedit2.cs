using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddDayCloseDeilsedit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPurchaseReturnAmount",
                table: "DayCloses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPurchaseReturnQty",
                table: "DayCloses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalesReturnAmount",
                table: "DayCloses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSalesReturnQty",
                table: "DayCloses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseAmount",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseQty",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseReturnAmount",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchaseReturnQty",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesAmount",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesQty",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesReturnAmount",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesReturnQty",
                table: "DayCloseDetailses",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPurchaseReturnAmount",
                table: "DayCloses");

            migrationBuilder.DropColumn(
                name: "TotalPurchaseReturnQty",
                table: "DayCloses");

            migrationBuilder.DropColumn(
                name: "TotalSalesReturnAmount",
                table: "DayCloses");

            migrationBuilder.DropColumn(
                name: "TotalSalesReturnQty",
                table: "DayCloses");

            migrationBuilder.DropColumn(
                name: "PurchaseAmount",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "PurchaseQty",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "PurchaseReturnAmount",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "PurchaseReturnQty",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "SalesAmount",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "SalesQty",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "SalesReturnAmount",
                table: "DayCloseDetailses");

            migrationBuilder.DropColumn(
                name: "SalesReturnQty",
                table: "DayCloseDetailses");
        }
    }
}
