using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AddColumnInTableSheetIteLiquidityGapAndLiqudityGapBucket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SummaryToBucket",
                table: "SheetItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "ScenarioNominal",
                table: "LiquidityGaps",
                type: "decimal(20,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Nominal",
                table: "LiquidityGaps",
                type: "decimal(20,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "ProposionalNominal",
                table: "LiquidityGaps",
                type: "decimal(20,4)",
                nullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ScenarioPercentage",
                table: "LiquidityGapBuckets",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "ActualPercentage",
                table: "LiquidityGapBuckets",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<decimal>(
                name: "ActualNominal",
                table: "LiquidityGapBuckets",
                type: "decimal(20,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProporsiNominal",
                table: "LiquidityGapBuckets",
                type: "decimal(20,4)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProporsiPercentage",
                table: "LiquidityGapBuckets",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ScenarioNominal",
                table: "LiquidityGapBuckets",
                type: "decimal(20,4)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SummaryToBucket",
                table: "SheetItems");

            migrationBuilder.DropColumn(
                name: "ProposionalNominal",
                table: "LiquidityGaps");

            migrationBuilder.DropColumn(
                name: "ActualNominal",
                table: "LiquidityGapBuckets");

            migrationBuilder.DropColumn(
                name: "ProporsiNominal",
                table: "LiquidityGapBuckets");

            migrationBuilder.DropColumn(
                name: "ProporsiPercentage",
                table: "LiquidityGapBuckets");

            migrationBuilder.DropColumn(
                name: "ScenarioNominal",
                table: "LiquidityGapBuckets");

            migrationBuilder.AlterColumn<decimal>(
                name: "ScenarioNominal",
                table: "LiquidityGaps",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Nominal",
                table: "LiquidityGaps",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,4)");

            migrationBuilder.AlterColumn<float>(
                name: "ScenarioPercentage",
                table: "LiquidityGapBuckets",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ActualPercentage",
                table: "LiquidityGapBuckets",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
