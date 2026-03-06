using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddYearClose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YearCloseDetails",
                columns: table => new
                {
                    YearCloseDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlNo = table.Column<int>(nullable: false),
                    YearName = table.Column<int>(nullable: false),
                    TranType = table.Column<string>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true),
                    SupplierId = table.Column<int>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    OpeningBalance = table.Column<decimal>(nullable: false),
                    ReceiveAmount = table.Column<decimal>(nullable: false),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    SalesAmount = table.Column<decimal>(nullable: false),
                    SalesQty = table.Column<decimal>(nullable: false),
                    PurchaseAmount = table.Column<decimal>(nullable: false),
                    PurchaseQty = table.Column<decimal>(nullable: false),
                    SalesReturnAmount = table.Column<decimal>(nullable: false),
                    SalesReturnQty = table.Column<decimal>(nullable: false),
                    ClosingBalance = table.Column<decimal>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsAllowEdit = table.Column<bool>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearCloseDetails", x => x.YearCloseDetailsID);
                    table.ForeignKey(
                        name: "FK_YearCloseDetails_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearCloseDetails_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearCloseDetails_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearCloseDetails_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearCloseDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearCloses",
                columns: table => new
                {
                    YearCloseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlNo = table.Column<int>(nullable: false),
                    YearName = table.Column<int>(nullable: false),
                    TranType = table.Column<string>(nullable: true),
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
                    TotalSalesReturnAmount = table.Column<decimal>(nullable: false),
                    TotalPurchaseReturnAmount = table.Column<decimal>(nullable: false),
                    TotalSalesReturnQty = table.Column<decimal>(nullable: false),
                    TotalPurchaseReturnQty = table.Column<decimal>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    IsAllowEdit = table.Column<bool>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearCloses", x => x.YearCloseID);
                    table.ForeignKey(
                        name: "FK_YearCloses_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearCloses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "YearItemStockCloses",
                columns: table => new
                {
                    YearItemStockCloseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SlNo = table.Column<int>(nullable: false),
                    YearName = table.Column<int>(nullable: false),
                    PrevStock = table.Column<decimal>(nullable: false),
                    CompanyProductID = table.Column<int>(nullable: false),
                    OpBalance = table.Column<decimal>(nullable: false),
                    PurchaseQty = table.Column<decimal>(nullable: false),
                    PurchaseAmount = table.Column<decimal>(nullable: false),
                    SalesQty = table.Column<decimal>(nullable: false),
                    SalesAmount = table.Column<decimal>(nullable: false),
                    ReturnQty = table.Column<decimal>(nullable: false),
                    ReturnAmount = table.Column<decimal>(nullable: false),
                    ClosingBalance = table.Column<decimal>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    CompanyID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsAllowEdit = table.Column<bool>(nullable: false),
                    LastUpdateTime = table.Column<DateTime>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearItemStockCloses", x => x.YearItemStockCloseID);
                    table.ForeignKey(
                        name: "FK_YearItemStockCloses_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearItemStockCloses_CompanyProducts_CompanyProductID",
                        column: x => x.CompanyProductID,
                        principalTable: "CompanyProducts",
                        principalColumn: "CompanyProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_YearItemStockCloses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YearCloseDetails_CompanyID",
                table: "YearCloseDetails",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_YearCloseDetails_CustomerID",
                table: "YearCloseDetails",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_YearCloseDetails_EmployeeID",
                table: "YearCloseDetails",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_YearCloseDetails_SupplierId",
                table: "YearCloseDetails",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_YearCloseDetails_UserId",
                table: "YearCloseDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_YearCloses_CompanyID",
                table: "YearCloses",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_YearCloses_UserId",
                table: "YearCloses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_YearItemStockCloses_CompanyID",
                table: "YearItemStockCloses",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_YearItemStockCloses_CompanyProductID",
                table: "YearItemStockCloses",
                column: "CompanyProductID");

            migrationBuilder.CreateIndex(
                name: "IX_YearItemStockCloses_UserId",
                table: "YearItemStockCloses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YearCloseDetails");

            migrationBuilder.DropTable(
                name: "YearCloses");

            migrationBuilder.DropTable(
                name: "YearItemStockCloses");
        }
    }
}
