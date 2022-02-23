using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bjb.LiquidityGap.Infrastructure.Persistence.Migrations
{
    public partial class ChangeColumNamePasswordToSandiInTableSummarySource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "SummarySources",
                newName: "Sandi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sandi",
                table: "SummarySources",
                newName: "Password");
        }
    }
}
