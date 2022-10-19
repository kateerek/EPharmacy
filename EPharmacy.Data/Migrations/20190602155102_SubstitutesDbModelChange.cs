using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPharmacy.Data.Migrations
{
    public partial class SubstitutesDbModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                defaultValue: new DateTime(2019, 6, 2, 17, 51, 2, 128, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 1, 18, 27, 33, 683, DateTimeKind.Local));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ActiveSubstances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ActiveSubstances");

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
                defaultValue: new DateTime(2019, 6, 1, 18, 27, 33, 683, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 2, 17, 51, 2, 128, DateTimeKind.Local));
        }
    }
}
