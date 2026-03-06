using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddColorReceiveAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionAmount",
                table: "SalesDetailss");

            migrationBuilder.DropColumn(
                name: "SalesAmount",
                table: "SalesDetailss");

            migrationBuilder.RenameColumn(
                name: "SalesQtyInDozen",
                table: "SalesDetailss",
                newName: "BuyRate");

            migrationBuilder.AddColumn<decimal>(
                name: "ReceiveAmount",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiveAmount",
                table: "SalesHeads");

            migrationBuilder.RenameColumn(
                name: "BuyRate",
                table: "SalesDetailss",
                newName: "SalesQtyInDozen");

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionAmount",
                table: "SalesDetailss",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesAmount",
                table: "SalesDetailss",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
