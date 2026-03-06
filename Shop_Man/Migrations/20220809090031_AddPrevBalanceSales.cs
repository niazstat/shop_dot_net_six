using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddPrevBalanceSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBalance",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousBalance",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ReturnQtyInPair",
                table: "SalesDetailss",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBalance",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "PreviousBalance",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "ReturnQtyInPair",
                table: "SalesDetailss");
        }
    }
}
