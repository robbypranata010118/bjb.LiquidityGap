using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AddTableCharacteristicTimeBucket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timebucket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timebucket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicTimebucket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharactericticId = table.Column<int>(type: "int", nullable: false),
                    TimebucketId = table.Column<int>(type: "int", nullable: false),
                    UsePercentage = table.Column<bool>(type: "bit", nullable: false),
                    DayRange = table.Column<int>(type: "int", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicTimebucket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicTimebucket_Characteristics_CharactericticId",
                        column: x => x.CharactericticId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacteristicTimebucket_Timebucket_TimebucketId",
                        column: x => x.TimebucketId,
                        principalTable: "Timebucket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicTimebucket_CharactericticId",
                table: "CharacteristicTimebucket",
                column: "CharactericticId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicTimebucket_TimebucketId",
                table: "CharacteristicTimebucket",
                column: "TimebucketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacteristicTimebucket");

            migrationBuilder.DropTable(
                name: "Timebucket");
        }
    }
}
