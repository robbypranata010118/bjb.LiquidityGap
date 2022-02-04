using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AddTableSheetItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SheetItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    DataSourceId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SheetItemParentId = table.Column<int>(type: "int", nullable: true),
                    MarkToCalculate = table.Column<bool>(type: "bit", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsManualInput = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetItems_DataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalTable: "DataSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheetItems_SheetItems_SheetItemParentId",
                        column: x => x.SheetItemParentId,
                        principalTable: "SheetItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SheetItems_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SheetItems_DataSourceId",
                table: "SheetItems",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetItems_SheetItemParentId",
                table: "SheetItems",
                column: "SheetItemParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetItems_SubCategoryId",
                table: "SheetItems",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetItems");
        }
    }
}
