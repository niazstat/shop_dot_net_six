using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCustomerNoString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerNo",
                table: "Customer",
                nullable: true,
                computedColumnSql: "CONVERT(VARCHAR, 1000 +  CustomerID )",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComputedColumnSql: "  1000+  CustomerID ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerNo",
                table: "Customer",
                nullable: true,
                computedColumnSql: "  1000+  CustomerID ",
                oldClrType: typeof(string),
                oldNullable: true,
                oldComputedColumnSql: "CONVERT(VARCHAR, 1000 +  CustomerID )");
        }
    }
}
