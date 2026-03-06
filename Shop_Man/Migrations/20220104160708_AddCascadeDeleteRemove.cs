using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Man.Migrations
{
    public partial class AddCascadeDeleteRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Company_CompanyID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Districts_DistrictID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Users_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubCategorys_CustomerCategorys_CustomerCategoryID",
                table: "CustomerSubCategorys");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Company_CompanyID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PermittedControllers_Users_UserId",
                table: "PermittedControllers");

            migrationBuilder.DropForeignKey(
                name: "FK_PermittedProjAction_ProjAction_ProjActionID",
                table: "PermittedProjAction");

            migrationBuilder.DropForeignKey(
                name: "FK_PermittedProjAction_Users_UserId",
                table: "PermittedProjAction");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Color_ColorID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Company_CompanyID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImage_ProductImageID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Size_SizeID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjAction_ProjControllers_ProjControllerID",
                table: "ProjAction");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Company_CompanyID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Users_UserId",
                table: "Suppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Company_CompanyID",
                table: "Customer",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Customer",
                column: "CustomerSubCategoryID",
                principalTable: "CustomerSubCategorys",
                principalColumn: "CustomerSubCategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Districts_DistrictID",
                table: "Customer",
                column: "DistrictID",
                principalTable: "Districts",
                principalColumn: "DistrictID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Users_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubCategorys_CustomerCategorys_CustomerCategoryID",
                table: "CustomerSubCategorys",
                column: "CustomerCategoryID",
                principalTable: "CustomerCategorys",
                principalColumn: "CustomerCategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Company_CompanyID",
                table: "Orders",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermittedControllers_Users_UserId",
                table: "PermittedControllers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermittedProjAction_ProjAction_ProjActionID",
                table: "PermittedProjAction",
                column: "ProjActionID",
                principalTable: "ProjAction",
                principalColumn: "ProjActionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PermittedProjAction_Users_UserId",
                table: "PermittedProjAction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Color_ColorID",
                table: "Products",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Company_CompanyID",
                table: "Products",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImage_ProductImageID",
                table: "Products",
                column: "ProductImageID",
                principalTable: "ProductImage",
                principalColumn: "ProductImageID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Size_SizeID",
                table: "Products",
                column: "SizeID",
                principalTable: "Size",
                principalColumn: "SizeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjAction_ProjControllers_ProjControllerID",
                table: "ProjAction",
                column: "ProjControllerID",
                principalTable: "ProjControllers",
                principalColumn: "ProjControllerID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Company_CompanyID",
                table: "Suppliers",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_UserId",
                table: "Suppliers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Company_CompanyID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Districts_DistrictID",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Users_UserId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSubCategorys_CustomerCategorys_CustomerCategoryID",
                table: "CustomerSubCategorys");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Company_CompanyID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PermittedControllers_Users_UserId",
                table: "PermittedControllers");

            migrationBuilder.DropForeignKey(
                name: "FK_PermittedProjAction_ProjAction_ProjActionID",
                table: "PermittedProjAction");

            migrationBuilder.DropForeignKey(
                name: "FK_PermittedProjAction_Users_UserId",
                table: "PermittedProjAction");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Color_ColorID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Company_CompanyID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductImage_ProductImageID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Size_SizeID",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjAction_ProjControllers_ProjControllerID",
                table: "ProjAction");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Company_CompanyID",
                table: "Suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Users_UserId",
                table: "Suppliers");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Company_CompanyID",
                table: "Customer",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerSubCategorys_CustomerSubCategoryID",
                table: "Customer",
                column: "CustomerSubCategoryID",
                principalTable: "CustomerSubCategorys",
                principalColumn: "CustomerSubCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Districts_DistrictID",
                table: "Customer",
                column: "DistrictID",
                principalTable: "Districts",
                principalColumn: "DistrictID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Users_UserId",
                table: "Customer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSubCategorys_CustomerCategorys_CustomerCategoryID",
                table: "CustomerSubCategorys",
                column: "CustomerCategoryID",
                principalTable: "CustomerCategorys",
                principalColumn: "CustomerCategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Company_CompanyID",
                table: "Orders",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermittedControllers_Users_UserId",
                table: "PermittedControllers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermittedProjAction_ProjAction_ProjActionID",
                table: "PermittedProjAction",
                column: "ProjActionID",
                principalTable: "ProjAction",
                principalColumn: "ProjActionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermittedProjAction_Users_UserId",
                table: "PermittedProjAction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Color_ColorID",
                table: "Products",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Company_CompanyID",
                table: "Products",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductImage_ProductImageID",
                table: "Products",
                column: "ProductImageID",
                principalTable: "ProductImage",
                principalColumn: "ProductImageID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Size_SizeID",
                table: "Products",
                column: "SizeID",
                principalTable: "Size",
                principalColumn: "SizeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjAction_ProjControllers_ProjControllerID",
                table: "ProjAction",
                column: "ProjControllerID",
                principalTable: "ProjControllers",
                principalColumn: "ProjControllerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Company_CompanyID",
                table: "Suppliers",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "CompanyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Users_UserId",
                table: "Suppliers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
