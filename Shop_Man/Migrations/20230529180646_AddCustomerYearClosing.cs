using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCustomerYearClosing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ClosingShortAmount",
                table: "Adjustments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RejectGoodsAmount",
                table: "Adjustments",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "YearCloseCustomerDetailses",
                columns: table => new
                {
                    YearCloseCustomerDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    AutoNo = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    dDate = table.Column<DateTime>(nullable: false),
                    OpeningBalance = table.Column<decimal>(nullable: false),
                    SalesAmount = table.Column<decimal>(nullable: false),
                    TotalSackNoFee = table.Column<decimal>(nullable: false),
                    TransportCost = table.Column<decimal>(nullable: false),
                    PackingCost = table.Column<decimal>(nullable: false),
                    AddLessAmount = table.Column<decimal>(nullable: false),
                    ReceiveAmount = table.Column<decimal>(nullable: false),
                    CashReceiveAmount = table.Column<decimal>(nullable: false),
                    CheckRecev = table.Column<decimal>(nullable: false),
                    CheckPayment = table.Column<decimal>(nullable: false),
                    CashPayment = table.Column<decimal>(nullable: false),
                    AdjustAmount = table.Column<decimal>(nullable: false),
                    ReturnAmount = table.Column<decimal>(nullable: false),
                    ClosingShortAmount = table.Column<decimal>(nullable: false),
                    RejectGoodsAmount = table.Column<decimal>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearCloseCustomerDetailses", x => x.YearCloseCustomerDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "YearCloseCustomerSumms",
                columns: table => new
                {
                    YearCloseCustomerSummID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerID = table.Column<int>(nullable: false),
                    YearName = table.Column<int>(nullable: false),
                    CustomerNo = table.Column<int>(nullable: false),
                    CustomerSubCategoryName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShopName = table.Column<string>(nullable: true),
                    OpeningBalance = table.Column<decimal>(nullable: false),
                    SalesAmount = table.Column<decimal>(nullable: false),
                    TotalSackNoFee = table.Column<decimal>(nullable: false),
                    TransportCost = table.Column<decimal>(nullable: false),
                    PackingCost = table.Column<decimal>(nullable: false),
                    AddLessAmount = table.Column<decimal>(nullable: false),
                    ReceiveAmount = table.Column<decimal>(nullable: false),
                    CashReceiveAmount = table.Column<decimal>(nullable: false),
                    CheckRecev = table.Column<decimal>(nullable: false),
                    CheckPayment = table.Column<decimal>(nullable: false),
                    CashPayment = table.Column<decimal>(nullable: false),
                    AdjustAmount = table.Column<decimal>(nullable: false),
                    ReturnAmount = table.Column<decimal>(nullable: false),
                    ClosingShortAmount = table.Column<decimal>(nullable: false),
                    RejectGoodsAmount = table.Column<decimal>(nullable: false),
                    ClosingAmount = table.Column<decimal>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearCloseCustomerSumms", x => x.YearCloseCustomerSummID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YearCloseCustomerDetailses");

            migrationBuilder.DropTable(
                name: "YearCloseCustomerSumms");

            migrationBuilder.DropColumn(
                name: "ClosingShortAmount",
                table: "Adjustments");

            migrationBuilder.DropColumn(
                name: "RejectGoodsAmount",
                table: "Adjustments");
        }
    }
}
