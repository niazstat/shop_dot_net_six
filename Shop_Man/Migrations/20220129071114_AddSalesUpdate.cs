using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSalesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesHeadID",
                table: "SalesDetailss",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailss_SalesHeadID",
                table: "SalesDetailss",
                column: "SalesHeadID");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesDetailss_SalesHeads_SalesHeadID",
                table: "SalesDetailss",
                column: "SalesHeadID",
                principalTable: "SalesHeads",
                principalColumn: "SalesHeadID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesDetailss_SalesHeads_SalesHeadID",
                table: "SalesDetailss");

            migrationBuilder.DropIndex(
                name: "IX_SalesDetailss_SalesHeadID",
                table: "SalesDetailss");

            migrationBuilder.DropColumn(
                name: "SalesHeadID",
                table: "SalesDetailss");
        }
    }
}
