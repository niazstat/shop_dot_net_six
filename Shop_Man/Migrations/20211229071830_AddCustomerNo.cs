using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCustomerNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "CustomerNoAutoNumbering",
                startValue: 1000L);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerNo",
                table: "Customer",
                nullable: true,
                computedColumnSql: "  1000+  CustomerID ",
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "CustomerNoAutoNumbering");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerNo",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true,
                oldComputedColumnSql: "  1000+  CustomerID ");
        }
    }
}
