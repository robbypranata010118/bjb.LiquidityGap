using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class AddTableCharacteristicFormula : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SheetItemCharacteristics_Characteristics_CharacteristicId",
                table: "SheetItemCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_SheetItemCharacteristics_SheetItems_SheetItemId",
                table: "SheetItemCharacteristics");

            migrationBuilder.AlterColumn<int>(
                name: "SheetItemId",
                table: "SheetItemCharacteristics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacteristicId",
                table: "SheetItemCharacteristics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CharacteristicFormula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacteristicId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Formula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserUp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicFormula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicFormula_Characteristics_CharacteristicId",
                        column: x => x.CharacteristicId,
                        principalTable: "Characteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicFormula_CharacteristicId",
                table: "CharacteristicFormula",
                column: "CharacteristicId");

            migrationBuilder.AddForeignKey(
                name: "FK_SheetItemCharacteristics_Characteristics_CharacteristicId",
                table: "SheetItemCharacteristics",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SheetItemCharacteristics_SheetItems_SheetItemId",
                table: "SheetItemCharacteristics",
                column: "SheetItemId",
                principalTable: "SheetItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SheetItemCharacteristics_Characteristics_CharacteristicId",
                table: "SheetItemCharacteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_SheetItemCharacteristics_SheetItems_SheetItemId",
                table: "SheetItemCharacteristics");

            migrationBuilder.DropTable(
                name: "CharacteristicFormula");

            migrationBuilder.AlterColumn<int>(
                name: "SheetItemId",
                table: "SheetItemCharacteristics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CharacteristicId",
                table: "SheetItemCharacteristics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SheetItemCharacteristics_Characteristics_CharacteristicId",
                table: "SheetItemCharacteristics",
                column: "CharacteristicId",
                principalTable: "Characteristics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SheetItemCharacteristics_SheetItems_SheetItemId",
                table: "SheetItemCharacteristics",
                column: "SheetItemId",
                principalTable: "SheetItems",
                principalColumn: "Id");
        }
    }
}
