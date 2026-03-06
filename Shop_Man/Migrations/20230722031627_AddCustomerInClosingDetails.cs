using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCustomerInClosingDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "YearCloseCustomerDetailses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_YearCloseCustomerDetailses_CustomerID",
                table: "YearCloseCustomerDetailses",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_YearCloseCustomerDetailses_Customer_CustomerID",
                table: "YearCloseCustomerDetailses",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YearCloseCustomerDetailses_Customer_CustomerID",
                table: "YearCloseCustomerDetailses");

            migrationBuilder.DropIndex(
                name: "IX_YearCloseCustomerDetailses_CustomerID",
                table: "YearCloseCustomerDetailses");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "YearCloseCustomerDetailses");
        }
    }
}
