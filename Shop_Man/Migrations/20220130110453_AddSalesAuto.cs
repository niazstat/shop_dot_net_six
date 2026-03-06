using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSalesAuto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeneratedSalesNo",
                table: "SalesHeads",
                nullable: true,
                computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,SalesDate))+'-'+CONVERT( VARCHAR,SalesNo)");

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeads_GeneratedSalesNo",
                table: "SalesHeads",
                column: "GeneratedSalesNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SalesHeads_GeneratedSalesNo",
                table: "SalesHeads");

            migrationBuilder.DropColumn(
                name: "GeneratedSalesNo",
                table: "SalesHeads");
        }
    }
}
