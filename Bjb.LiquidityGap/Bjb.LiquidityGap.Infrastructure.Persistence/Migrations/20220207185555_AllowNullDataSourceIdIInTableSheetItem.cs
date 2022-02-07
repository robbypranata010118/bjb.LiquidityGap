using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AllowNullDataSourceIdIInTableSheetItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SheetItems_DataSources_DataSourceId",
                table: "SheetItems");

            migrationBuilder.AlterColumn<int>(
                name: "DataSourceId",
                table: "SheetItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SheetItems_DataSources_DataSourceId",
                table: "SheetItems",
                column: "DataSourceId",
                principalTable: "DataSources",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SheetItems_DataSources_DataSourceId",
                table: "SheetItems");

            migrationBuilder.AlterColumn<int>(
                name: "DataSourceId",
                table: "SheetItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SheetItems_DataSources_DataSourceId",
                table: "SheetItems",
                column: "DataSourceId",
                principalTable: "DataSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
