using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AddTableSheetItemTimebuckets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SheetItemTimebuckets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SheetItemId = table.Column<int>(type: "int", nullable: false),
                    TimebucketId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetItemTimebuckets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetItemTimebuckets_SheetItems_SheetItemId",
                        column: x => x.SheetItemId,
                        principalTable: "SheetItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheetItemTimebuckets_Timebuckets_TimebucketId",
                        column: x => x.TimebucketId,
                        principalTable: "Timebuckets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SheetItemTimebuckets_IsActive",
                table: "SheetItemTimebuckets",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SheetItemTimebuckets_SheetItemId",
                table: "SheetItemTimebuckets",
                column: "SheetItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetItemTimebuckets_TimebucketId",
                table: "SheetItemTimebuckets",
                column: "TimebucketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetItemTimebuckets");
        }
    }
}
