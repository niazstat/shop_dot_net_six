using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseHeads",
                columns: table => new
                {
                    PurchaseHeadID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseHeadNo = table.Column<string>(nullable: true),
                    AutoPurchaseHeadNo = table.Column<int>(nullable: false),
                    GeneratedPurchaseHeadNo = table.Column<string>(nullable: true, computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,PurchaseDate))+'-'+CONVERT( VARCHAR,AutoPurchaseHeadNo)"),
                    IsCashPurchase = table.Column<bool>(nullable: false),
                    SupplierId = table.Column<int>(nullable: true),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    ReceiveAmount = table.Column<decimal>(nullable: false),
                    TotalCommission = table.Column<decimal>(nullable: false),
                    TransportCost = table.Column<decimal>(nullable: false),
                    TotalNetAmount = table.Column<decimal>(nullable: false),
                    PaymentMediumID = table.Column<int>(nullable: false),
                    Note1 = table.Column<string>(nullable: true),
                    Note2 = table.Column<string>(nullable: true),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseHeads", x => x.PurchaseHeadID);
                    table.ForeignKey(
                        name: "FK_PurchaseHeads_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseHeads_PaymentMediums_PaymentMediumID",
                        column: x => x.PaymentMediumID,
                        principalTable: "PaymentMediums",
                        principalColumn: "PaymentMediumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseHeads_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseHeads_Company_UserId",
                        column: x => x.UserId,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    PurchaseDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyProductID = table.Column<int>(nullable: false),
                    PurchaseHeadID = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProdNameID = table.Column<int>(nullable: true),
                    ArticleName = table.Column<string>(nullable: true),
                    ArticleID = table.Column<int>(nullable: true),
                    SizeName = table.Column<string>(nullable: true),
                    SizeID = table.Column<int>(nullable: true),
                    UOMName = table.Column<string>(nullable: true),
                    UOMID = table.Column<int>(nullable: true),
                    CurrentStockQty = table.Column<decimal>(nullable: false),
                    PurchaseQtyInPair = table.Column<decimal>(nullable: false),
                    CommissionRate = table.Column<decimal>(nullable: false),
                    PurchaseRate = table.Column<decimal>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.PurchaseDetailsID);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_CompanyProducts_CompanyProductID",
                        column: x => x.CompanyProductID,
                        principalTable: "CompanyProducts",
                        principalColumn: "CompanyProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_ProdNames_ProdNameID",
                        column: x => x.ProdNameID,
                        principalTable: "ProdNames",
                        principalColumn: "ProdNameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_PurchaseHeads_PurchaseHeadID",
                        column: x => x.PurchaseHeadID,
                        principalTable: "PurchaseHeads",
                        principalColumn: "PurchaseHeadID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Size_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Size",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_UOMs_UOMID",
                        column: x => x.UOMID,
                        principalTable: "UOMs",
                        principalColumn: "UOMID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_ArticleID",
                table: "PurchaseDetails",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_CompanyID",
                table: "PurchaseDetails",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_CompanyProductID",
                table: "PurchaseDetails",
                column: "CompanyProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_ProdNameID",
                table: "PurchaseDetails",
                column: "ProdNameID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_PurchaseHeadID",
                table: "PurchaseDetails",
                column: "PurchaseHeadID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_SizeID",
                table: "PurchaseDetails",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_UOMID",
                table: "PurchaseDetails",
                column: "UOMID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHeads_CompanyID",
                table: "PurchaseHeads",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHeads_GeneratedPurchaseHeadNo",
                table: "PurchaseHeads",
                column: "GeneratedPurchaseHeadNo");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHeads_PaymentMediumID",
                table: "PurchaseHeads",
                column: "PaymentMediumID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHeads_SupplierId",
                table: "PurchaseHeads",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHeads_UserId",
                table: "PurchaseHeads",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "PurchaseHeads");
        }
    }
}
