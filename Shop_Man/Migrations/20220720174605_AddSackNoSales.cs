using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSackNoSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AddLessAmount",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSackNo",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSackNoFee",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddLessAmount",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "TotalSackNo",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "TotalSackNoFee",
                table: "SalesHeads");
        }
    }
}
