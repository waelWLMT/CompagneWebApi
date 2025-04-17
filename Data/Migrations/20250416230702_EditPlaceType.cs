using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class EditPlaceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Place_TagKeyCode",
                table: "CompaignBusinesses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place_TagValueCode",
                table: "CompaignBusinesses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place_TagKeyCode",
                table: "CompaignBusinesses");

            migrationBuilder.DropColumn(
                name: "Place_TagValueCode",
                table: "CompaignBusinesses");
        }
    }
}
