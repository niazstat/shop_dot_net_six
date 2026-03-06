using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddYearDetails3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YearItemStockCloseDetails",
                columns: table => new
                {
                    YearItemStockCloseDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    YearName = table.Column<int>(nullable: false),
                    dDate = table.Column<DateTime>(nullable: false),
                    TranType = table.Column<string>(nullable: true),
                    CompanyProductID = table.Column<int>(nullable: true),
                    ArticleName = table.Column<string>(nullable: true),
                    SizeName = table.Column<string>(nullable: true),
                    PurInv = table.Column<string>(nullable: true),
                    PurQty = table.Column<decimal>(nullable: false),
                    NetPurRate = table.Column<decimal>(nullable: false),
                    NetPurAmount = table.Column<decimal>(nullable: false),
                    SalesInv = table.Column<string>(nullable: true),
                    SalesQty = table.Column<decimal>(nullable: false),
                    NetSalesRate = table.Column<decimal>(nullable: false),
                    NetSalesAmount = table.Column<decimal>(nullable: false),
                    RetInvNo = table.Column<string>(nullable: true),
                    RetQtyQty = table.Column<decimal>(nullable: false),
                    NetRetRate = table.Column<decimal>(nullable: false),
                    NetRetAmount = table.Column<decimal>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsAllowEdit = table.Column<bool>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearItemStockCloseDetails", x => x.YearItemStockCloseDetailsID);
                    table.ForeignKey(
                        name: "FK_YearItemStockCloseDetails_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearItemStockCloseDetails_CompanyProducts_CompanyProductID",
                        column: x => x.CompanyProductID,
                        principalTable: "CompanyProducts",
                        principalColumn: "CompanyProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearItemStockCloseDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YearItemStockCloseDetails_CompanyID",
                table: "YearItemStockCloseDetails",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_YearItemStockCloseDetails_CompanyProductID",
                table: "YearItemStockCloseDetails",
                column: "CompanyProductID");

            migrationBuilder.CreateIndex(
                name: "IX_YearItemStockCloseDetails_UserId",
                table: "YearItemStockCloseDetails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YearItemStockCloseDetails");
        }
    }
}
