using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateBusinessTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MapCode",
                table: "businessTypes",
                newName: "Label");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "businessTypes",
                newName: "GoogleMapPlaceCode");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "businessTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoApiPlaceCategry",
                table: "businessTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GeoApiPlaceCode",
                table: "businessTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "businessTypes");

            migrationBuilder.DropColumn(
                name: "GeoApiPlaceCategry",
                table: "businessTypes");

            migrationBuilder.DropColumn(
                name: "GeoApiPlaceCode",
                table: "businessTypes");

            migrationBuilder.RenameColumn(
                name: "Label",
                table: "businessTypes",
                newName: "MapCode");

            migrationBuilder.RenameColumn(
                name: "GoogleMapPlaceCode",
                table: "businessTypes",
                newName: "Code");
        }
    }
}
