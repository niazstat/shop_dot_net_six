using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCSalesReturn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesReturns",
                columns: table => new
                {
                    SalesReturnID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReturnNo = table.Column<string>(nullable: true),
                    AutoReturnNo = table.Column<int>(nullable: false),
                    CustwiseReturnNo = table.Column<int>(nullable: false),
                    GeneratedReturnNo = table.Column<string>(nullable: true, computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,ReturnDate))+'-'+CONVERT( VARCHAR,AutoReturnNo)"),
                    CustomerID = table.Column<int>(nullable: true),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    Note1 = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturns", x => x.SalesReturnID);
                    table.ForeignKey(
                        name: "FK_SalesReturns_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturns_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturns_Company_UserId",
                        column: x => x.UserId,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesReturnDetails",
                columns: table => new
                {
                    SalesReturnDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyProductID = table.Column<int>(nullable: false),
                    SalesReturnID = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProdNameID = table.Column<int>(nullable: true),
                    ArticleName = table.Column<string>(nullable: true),
                    ArticleID = table.Column<int>(nullable: true),
                    SizeName = table.Column<string>(nullable: true),
                    SizeID = table.Column<int>(nullable: true),
                    UOMName = table.Column<string>(nullable: true),
                    UOMID = table.Column<int>(nullable: true),
                    CurrentStockQty = table.Column<decimal>(nullable: false),
                    ReyurnQtyInPair = table.Column<decimal>(nullable: false),
                    ReturnCommissionRate = table.Column<decimal>(nullable: false),
                    RetRate = table.Column<decimal>(nullable: false),
                    SalesCommissionRate = table.Column<decimal>(nullable: false),
                    SalesRate = table.Column<decimal>(nullable: false),
                    BuyRate = table.Column<decimal>(nullable: false),
                    BuyCommRate = table.Column<decimal>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    SalesDetailsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturnDetails", x => x.SalesReturnDetailsID);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_CompanyProducts_CompanyProductID",
                        column: x => x.CompanyProductID,
                        principalTable: "CompanyProducts",
                        principalColumn: "CompanyProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_ProdNames_ProdNameID",
                        column: x => x.ProdNameID,
                        principalTable: "ProdNames",
                        principalColumn: "ProdNameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_SalesDetailss_SalesDetailsID",
                        column: x => x.SalesDetailsID,
                        principalTable: "SalesDetailss",
                        principalColumn: "SalesDetailsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_SalesReturns_SalesReturnID",
                        column: x => x.SalesReturnID,
                        principalTable: "SalesReturns",
                        principalColumn: "SalesReturnID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_Size_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Size",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReturnDetails_UOMs_UOMID",
                        column: x => x.UOMID,
                        principalTable: "UOMs",
                        principalColumn: "UOMID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_ArticleID",
                table: "SalesReturnDetails",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_CompanyID",
                table: "SalesReturnDetails",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_CompanyProductID",
                table: "SalesReturnDetails",
                column: "CompanyProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_ProdNameID",
                table: "SalesReturnDetails",
                column: "ProdNameID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_SalesDetailsID",
                table: "SalesReturnDetails",
                column: "SalesDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_SalesReturnID",
                table: "SalesReturnDetails",
                column: "SalesReturnID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_SizeID",
                table: "SalesReturnDetails",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturnDetails_UOMID",
                table: "SalesReturnDetails",
                column: "UOMID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_CompanyID",
                table: "SalesReturns",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_CustomerID",
                table: "SalesReturns",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_GeneratedReturnNo",
                table: "SalesReturns",
                column: "GeneratedReturnNo");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturns_UserId",
                table: "SalesReturns",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesReturnDetails");

            migrationBuilder.DropTable(
                name: "SalesReturns");
        }
    }
}
