using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addFinalUnitProductPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FinalUnitPrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalUnitPrice",
                table: "Products");
        }
    }
}
