using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddSalesTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentMediums",
                columns: table => new
                {
                    PaymentMediumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMediums", x => x.PaymentMediumID);
                    table.ForeignKey(
                        name: "FK_PaymentMediums_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesDetailss",
                columns: table => new
                {
                    SalesDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyProductID = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProdNameID = table.Column<int>(nullable: true),
                    ArticleName = table.Column<string>(nullable: true),
                    ArticleID = table.Column<int>(nullable: true),
                    SizeName = table.Column<string>(nullable: true),
                    SizeID = table.Column<int>(nullable: true),
                    UOMName = table.Column<string>(nullable: true),
                    UOMID = table.Column<int>(nullable: true),
                    CurrentStockQty = table.Column<decimal>(nullable: false),
                    SalesQtyInDozen = table.Column<decimal>(nullable: false),
                    SalesQtyInPair = table.Column<decimal>(nullable: false),
                    CommissionRate = table.Column<decimal>(nullable: false),
                    CommissionAmount = table.Column<decimal>(nullable: false),
                    SalesRate = table.Column<decimal>(nullable: false),
                    SalesAmount = table.Column<decimal>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDetailss", x => x.SalesDetailsID);
                    table.ForeignKey(
                        name: "FK_SalesDetailss_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDetailss_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDetailss_CompanyProducts_CompanyProductID",
                        column: x => x.CompanyProductID,
                        principalTable: "CompanyProducts",
                        principalColumn: "CompanyProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDetailss_ProdNames_ProdNameID",
                        column: x => x.ProdNameID,
                        principalTable: "ProdNames",
                        principalColumn: "ProdNameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDetailss_Size_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Size",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDetailss_UOMs_UOMID",
                        column: x => x.UOMID,
                        principalTable: "UOMs",
                        principalColumn: "UOMID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesHeads",
                columns: table => new
                {
                    SalesHeadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SalesNo = table.Column<string>(nullable: true),
                    IsCashSales = table.Column<bool>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true),
                    SalesDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalCommission = table.Column<decimal>(nullable: false),
                    TransportCost = table.Column<decimal>(nullable: false),
                    PackingCost = table.Column<decimal>(nullable: false),
                    TotalNetAmount = table.Column<decimal>(nullable: false),
                    PaymentMediumID = table.Column<int>(nullable: false),
                    AccNo = table.Column<string>(nullable: true),
                    CheckNo = table.Column<string>(nullable: true),
                    CheckPassDate = table.Column<DateTime>(nullable: false),
                    Note1 = table.Column<string>(nullable: true),
                    Note2 = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesHeads", x => x.SalesHeadID);
                    table.ForeignKey(
                        name: "FK_SalesHeads_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesHeads_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesHeads_PaymentMediums_PaymentMediumID",
                        column: x => x.PaymentMediumID,
                        principalTable: "PaymentMediums",
                        principalColumn: "PaymentMediumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesHeads_Company_UserId",
                        column: x => x.UserId,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMediums_CompanyID",
                table: "PaymentMediums",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailss_ArticleID",
                table: "SalesDetailss",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailss_CompanyID",
                table: "SalesDetailss",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailss_CompanyProductID",
                table: "SalesDetailss",
                column: "CompanyProductID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailss_ProdNameID",
                table: "SalesDetailss",
                column: "ProdNameID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailss_SizeID",
                table: "SalesDetailss",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDetailss_UOMID",
                table: "SalesDetailss",
                column: "UOMID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeads_CompanyID",
                table: "SalesHeads",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeads_CustomerID",
                table: "SalesHeads",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeads_PaymentMediumID",
                table: "SalesHeads",
                column: "PaymentMediumID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesHeads_UserId",
                table: "SalesHeads",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesDetailss");

            migrationBuilder.DropTable(
                name: "SalesHeads");

            migrationBuilder.DropTable(
                name: "PaymentMediums");
        }
    }
}
