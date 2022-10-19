using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPharmacy.Data.Migrations
{
    public partial class AddPriceWithDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "SalesOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "SalesOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DiscountCategoryId",
                table: "ProductItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceWithDiscount",
                table: "ProductItems",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidTo",
                table: "Discounts",
                nullable: false,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "Discounts",
                nullable: false,
                defaultValue: new DateTime(2019, 6, 3, 1, 10, 33, 75, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 1, 14, 32, 6, 734, DateTimeKind.Local));

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_DiscountCategoryId",
                table: "ProductItems",
                column: "DiscountCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_DiscountCategories_DiscountCategoryId",
                table: "ProductItems",
                column: "DiscountCategoryId",
                principalTable: "DiscountCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_DiscountCategories_DiscountCategoryId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_DiscountCategoryId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DiscountCategoryId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "PriceWithDiscount",
                table: "ProductItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidTo",
                table: "Discounts",
                nullable: false,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidFrom",
                table: "Discounts",
                nullable: false,
                defaultValue: new DateTime(2019, 6, 1, 14, 32, 6, 734, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 3, 1, 10, 33, 75, DateTimeKind.Local));
        }
    }
}
