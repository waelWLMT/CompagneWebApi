using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DefaultPenetraionRatePerBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultNbrProductPerBusiness",
                table: "ProductTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultNbrProductPerBusiness",
                table: "ProductTypes");
        }
    }
}
