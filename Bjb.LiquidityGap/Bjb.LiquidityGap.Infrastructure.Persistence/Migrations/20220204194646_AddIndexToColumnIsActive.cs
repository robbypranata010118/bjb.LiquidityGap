using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AddIndexToColumnIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Timebucket",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SummarySources",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SubCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SheetItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SheetItemCharacteristics",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "LiquidityGapBucket",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "LiquidityGap",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "DataSources",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Currencies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "CharacteristicTimebucket",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Characteristics",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "CharacteristicFormula",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "AuditTrails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timebucket_IsActive",
                table: "Timebucket",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SummarySources_IsActive",
                table: "SummarySources",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_IsActive",
                table: "SubCategories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SheetItems_IsActive",
                table: "SheetItems",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SheetItemCharacteristics_IsActive",
                table: "SheetItemCharacteristics",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidityGapBucket_IsActive",
                table: "LiquidityGapBucket",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidityGap_IsActive",
                table: "LiquidityGap",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_IsActive",
                table: "DataSources",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_IsActive",
                table: "Currencies",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicTimebucket_IsActive",
                table: "CharacteristicTimebucket",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_IsActive",
                table: "Characteristics",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicFormula_IsActive",
                table: "CharacteristicFormula",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_IsActive",
                table: "Categories",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AuditTrails_IsActive",
                table: "AuditTrails",
                column: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Timebucket_IsActive",
                table: "Timebucket");

            migrationBuilder.DropIndex(
                name: "IX_SummarySources_IsActive",
                table: "SummarySources");

            migrationBuilder.DropIndex(
                name: "IX_SubCategories_IsActive",
                table: "SubCategories");

            migrationBuilder.DropIndex(
                name: "IX_SheetItems_IsActive",
                table: "SheetItems");

            migrationBuilder.DropIndex(
                name: "IX_SheetItemCharacteristics_IsActive",
                table: "SheetItemCharacteristics");

            migrationBuilder.DropIndex(
                name: "IX_LiquidityGapBucket_IsActive",
                table: "LiquidityGapBucket");

            migrationBuilder.DropIndex(
                name: "IX_LiquidityGap_IsActive",
                table: "LiquidityGap");

            migrationBuilder.DropIndex(
                name: "IX_DataSources_IsActive",
                table: "DataSources");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_IsActive",
                table: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicTimebucket_IsActive",
                table: "CharacteristicTimebucket");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_IsActive",
                table: "Characteristics");

            migrationBuilder.DropIndex(
                name: "IX_CharacteristicFormula_IsActive",
                table: "CharacteristicFormula");

            migrationBuilder.DropIndex(
                name: "IX_Categories_IsActive",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_AuditTrails_IsActive",
                table: "AuditTrails");

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Timebucket",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SummarySources",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SubCategories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SheetItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "SheetItemCharacteristics",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "LiquidityGapBucket",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "LiquidityGap",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "DataSources",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Currencies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "CharacteristicTimebucket",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Characteristics",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "CharacteristicFormula",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserIn",
                table: "AuditTrails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
