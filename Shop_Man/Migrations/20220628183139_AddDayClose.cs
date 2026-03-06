using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddDayClose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayCloses",
                columns: table => new
                {
                    DayCloseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlNo = table.Column<int>(nullable: false),
                    dDate = table.Column<DateTime>(nullable: false),
                    PrevBalance = table.Column<decimal>(nullable: false),
                    AddLess = table.Column<decimal>(nullable: false),
                    TotalSales = table.Column<decimal>(nullable: false),
                    TotalPurchase = table.Column<decimal>(nullable: false),
                    TotalSalesQty = table.Column<decimal>(nullable: false),
                    TotalPurchaseQty = table.Column<decimal>(nullable: false),
                    CashReceive = table.Column<decimal>(nullable: false),
                    BkashReceive = table.Column<decimal>(nullable: false),
                    ChequeReceive = table.Column<decimal>(nullable: false),
                    OthersReceive = table.Column<decimal>(nullable: false),
                    CashPayment = table.Column<decimal>(nullable: false),
                    Bkashpayment = table.Column<decimal>(nullable: false),
                    ChequePayment = table.Column<decimal>(nullable: false),
                    OthersPayment = table.Column<decimal>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayCloses", x => x.DayCloseID);
                    table.ForeignKey(
                        name: "FK_DayCloses_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DayCloses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayCloses_CompanyID",
                table: "DayCloses",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DayCloses_UserId",
                table: "DayCloses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayCloses");
        }
    }
}
