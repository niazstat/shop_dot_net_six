using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddBankTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    AccountTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.AccountTypeID);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    BankAccountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Startdate = table.Column<DateTime>(nullable: false),
                    AccountHolderName = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    AccountTypeName = table.Column<string>(nullable: true),
                    AccointNo = table.Column<string>(nullable: true),
                    StartingBalance = table.Column<decimal>(nullable: false),
                    CurrentBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.BankAccountID);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankID);
                });

            migrationBuilder.CreateTable(
                name: "CashPayments",
                columns: table => new
                {
                    CashPaymentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoicNo = table.Column<string>(nullable: true, computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,PaymentDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)"),
                    InvoicNoSL = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    SupplierId = table.Column<int>(nullable: true),
                    PaymentMediumID = table.Column<int>(nullable: true),
                    BankAccountID = table.Column<int>(nullable: true),
                    PreviousDue = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashPayments", x => x.CashPaymentID);
                    table.ForeignKey(
                        name: "FK_CashPayments_BankAccounts_BankAccountID",
                        column: x => x.BankAccountID,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashPayments_PaymentMediums_PaymentMediumID",
                        column: x => x.PaymentMediumID,
                        principalTable: "PaymentMediums",
                        principalColumn: "PaymentMediumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashPayments_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashPayments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashReceive",
                columns: table => new
                {
                    CashReceiveID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoicNo = table.Column<string>(nullable: true, computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,ReceiveDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)"),
                    InvoicNoSL = table.Column<int>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    BankAccountID = table.Column<int>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true),
                    PaymentMediumID = table.Column<int>(nullable: true),
                    PreviousDue = table.Column<decimal>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashReceive", x => x.CashReceiveID);
                    table.ForeignKey(
                        name: "FK_CashReceive_BankAccounts_BankAccountID",
                        column: x => x.BankAccountID,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashReceive_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashReceive_PaymentMediums_PaymentMediumID",
                        column: x => x.PaymentMediumID,
                        principalTable: "PaymentMediums",
                        principalColumn: "PaymentMediumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashReceive_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChequeTransactions",
                columns: table => new
                {
                    ChequeTransactionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoicNo = table.Column<string>(nullable: true, computedColumnSql: "CONVERT( VARCHAR,DATEPART(YEAR,TranDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)"),
                    InvoicNoSL = table.Column<int>(nullable: false),
                    TranDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    PaymentMediumID = table.Column<int>(nullable: true),
                    LedgerName = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    BankAccountID = table.Column<int>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    ChequeTTNo = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    IsChequePassed = table.Column<bool>(nullable: false),
                    ChequePassDate = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeTransactions", x => x.ChequeTransactionID);
                    table.ForeignKey(
                        name: "FK_ChequeTransactions_BankAccounts_BankAccountID",
                        column: x => x.BankAccountID,
                        principalTable: "BankAccounts",
                        principalColumn: "BankAccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChequeTransactions_PaymentMediums_PaymentMediumID",
                        column: x => x.PaymentMediumID,
                        principalTable: "PaymentMediums",
                        principalColumn: "PaymentMediumID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChequeTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_BankAccountID",
                table: "CashPayments",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_PaymentMediumID",
                table: "CashPayments",
                column: "PaymentMediumID");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_SupplierId",
                table: "CashPayments",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_UserId",
                table: "CashPayments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceive_BankAccountID",
                table: "CashReceive",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceive_CustomerID",
                table: "CashReceive",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceive_PaymentMediumID",
                table: "CashReceive",
                column: "PaymentMediumID");

            migrationBuilder.CreateIndex(
                name: "IX_CashReceive_UserId",
                table: "CashReceive",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeTransactions_BankAccountID",
                table: "ChequeTransactions",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeTransactions_PaymentMediumID",
                table: "ChequeTransactions",
                column: "PaymentMediumID");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeTransactions_UserId",
                table: "ChequeTransactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "CashPayments");

            migrationBuilder.DropTable(
                name: "CashReceive");

            migrationBuilder.DropTable(
                name: "ChequeTransactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}
