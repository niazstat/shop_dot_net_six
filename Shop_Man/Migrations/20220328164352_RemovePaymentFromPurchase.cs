using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class RemovePaymentFromPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHeads_PaymentMediums_PaymentMediumID",
                table: "PurchaseHeads");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseHeads_PaymentMediumID",
                table: "PurchaseHeads");

            migrationBuilder.DropColumn(
                name: "PaymentMediumID",
                table: "PurchaseHeads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMediumID",
                table: "PurchaseHeads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseHeads_PaymentMediumID",
                table: "PurchaseHeads",
                column: "PaymentMediumID");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHeads_PaymentMediums_PaymentMediumID",
                table: "PurchaseHeads",
                column: "PaymentMediumID",
                principalTable: "PaymentMediums",
                principalColumn: "PaymentMediumID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
