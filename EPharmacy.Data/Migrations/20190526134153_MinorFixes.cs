using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPharmacy.Data.Migrations
{
    public partial class MinorFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                nullable: true,
                defaultValue: "https://ziwgstorage.blob.core.windows.net/product-images/57566155-5596-4d75-9a35-faf89de02153image_placeholder.png",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "https://ziwgstorage.blob.core.windows.net/product-images/eb6d1046-ce98-49f1-bff7-c6deb2ee14d7diohespan_max.jpg");

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
                defaultValue: new DateTime(2019, 5, 26, 15, 41, 53, 266, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 11, 11, 16, 551, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                nullable: true,
                defaultValue: "https://ziwgstorage.blob.core.windows.net/product-images/eb6d1046-ce98-49f1-bff7-c6deb2ee14d7diohespan_max.jpg",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "https://ziwgstorage.blob.core.windows.net/product-images/57566155-5596-4d75-9a35-faf89de02153image_placeholder.png");

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
                defaultValue: new DateTime(2019, 5, 26, 11, 11, 16, 551, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 15, 41, 53, 266, DateTimeKind.Local));

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValues_Attributes_AttributeId",
                table: "AttributeValues",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
