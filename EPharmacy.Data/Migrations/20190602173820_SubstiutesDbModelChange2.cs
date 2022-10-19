using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPharmacy.Data.Migrations
{
    public partial class SubstiutesDbModelChange2 : Migration
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
                defaultValue: new DateTime(2019, 6, 2, 19, 38, 19, 969, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 6, 2, 17, 51, 2, 128, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ActiveSubstances",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "InternalName",
                table: "ActiveSubstances",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                oldDefaultValue: new DateTime(2019, 6, 2, 19, 38, 19, 969, DateTimeKind.Local));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ActiveSubstances",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "InternalName",
                table: "ActiveSubstances",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 800);
        }
    }
}
