using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddComPanyProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyProducts",
                columns: table => new
                {
                    CompanyProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupplierId = table.Column<int>(nullable: true),
                    ProdNameID = table.Column<int>(nullable: true),
                    ArticleID = table.Column<int>(nullable: true),
                    ProdCoCategoryID = table.Column<int>(nullable: true),
                    SizeID = table.Column<int>(nullable: true),
                    ProdTypeID = table.Column<int>(nullable: true),
                    UOMID = table.Column<int>(nullable: true),
                    BuyPrice = table.Column<decimal>(nullable: false),
                    BuyComm = table.Column<decimal>(nullable: false),
                    SellPrice = table.Column<decimal>(nullable: false),
                    SellComm = table.Column<decimal>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    OpeningStock = table.Column<decimal>(nullable: false),
                    CurrentStock = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProducts", x => x.CompanyProductID);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_Articles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "Articles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_ProdCoCategorys_ProdCoCategoryID",
                        column: x => x.ProdCoCategoryID,
                        principalTable: "ProdCoCategorys",
                        principalColumn: "ProdCoCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_ProdNames_ProdNameID",
                        column: x => x.ProdNameID,
                        principalTable: "ProdNames",
                        principalColumn: "ProdNameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_ProdTypes_ProdTypeID",
                        column: x => x.ProdTypeID,
                        principalTable: "ProdTypes",
                        principalColumn: "ProdTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_Size_SizeID",
                        column: x => x.SizeID,
                        principalTable: "Size",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyProducts_UOMs_UOMID",
                        column: x => x.UOMID,
                        principalTable: "UOMs",
                        principalColumn: "UOMID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_ArticleID",
                table: "CompanyProducts",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_CompanyID",
                table: "CompanyProducts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_ProdCoCategoryID",
                table: "CompanyProducts",
                column: "ProdCoCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_ProdNameID",
                table: "CompanyProducts",
                column: "ProdNameID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_ProdTypeID",
                table: "CompanyProducts",
                column: "ProdTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_SizeID",
                table: "CompanyProducts",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_SupplierId",
                table: "CompanyProducts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProducts_UOMID",
                table: "CompanyProducts",
                column: "UOMID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyProducts");
        }
    }
}
