using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class modifbusinessQuoteLineEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NbrBusinessTypePerCampagne",
                table: "BusinessTypeQuoteLines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NbrBusinessTypePerCampagne",
                table: "BusinessTypeQuoteLines");
        }
    }
}
