using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AddTableLiquidityGapAndLiquidityGapBucket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LiquidityGap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BussinessDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SheetItemId = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nominal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ScenarioNominal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidityGap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiquidityGap_SheetItems_SheetItemId",
                        column: x => x.SheetItemId,
                        principalTable: "SheetItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LiquidityGapBucket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LiquidityGapId = table.Column<int>(type: "int", nullable: false),
                    TimeBucketId = table.Column<int>(type: "int", nullable: false),
                    ActualPercentage = table.Column<float>(type: "real", nullable: false),
                    ActualCalc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScenarioCalc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScenarioPercentage = table.Column<float>(type: "real", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquidityGapBucket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiquidityGapBucket_LiquidityGap_LiquidityGapId",
                        column: x => x.LiquidityGapId,
                        principalTable: "LiquidityGap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LiquidityGapBucket_Timebucket_TimeBucketId",
                        column: x => x.TimeBucketId,
                        principalTable: "Timebucket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiquidityGap_SheetItemId",
                table: "LiquidityGap",
                column: "SheetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidityGapBucket_LiquidityGapId",
                table: "LiquidityGapBucket",
                column: "LiquidityGapId");

            migrationBuilder.CreateIndex(
                name: "IX_LiquidityGapBucket_TimeBucketId",
                table: "LiquidityGapBucket",
                column: "TimeBucketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiquidityGapBucket");

            migrationBuilder.DropTable(
                name: "LiquidityGap");
        }
    }
}
