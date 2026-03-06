using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSalesNoInt2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           // migrationBuilder.AlterColumn<string>(
               // name: "SalesNo",
               // table: "SalesHeads",
                //nullable: true,
                //oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AutoSalesNo",
                table: "SalesHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "GeneratedSalesNo",
                table: "SalesHeads",
                nullable: true,
                computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,SalesDate))+'-'+CONVERT( VARCHAR,AutoSalesNo)",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComputedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,SalesDate))+'-'+CONVERT( VARCHAR,SalesNo)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoSalesNo",
                table: "SalesHeads");

            migrationBuilder.AlterColumn<int>(
                name: "SalesNo",
                table: "SalesHeads",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GeneratedSalesNo",
                table: "SalesHeads",
                nullable: true,
                computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,SalesDate))+'-'+CONVERT( VARCHAR,SalesNo)",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComputedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,SalesDate))+'-'+CONVERT( VARCHAR,AutoSalesNo)");
        }
    }
}
