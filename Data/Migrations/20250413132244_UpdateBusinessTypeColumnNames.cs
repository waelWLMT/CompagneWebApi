using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UpdateBusinessTypeColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Label",
                table: "businessTypes",
                newName: "MapCode");

            migrationBuilder.RenameColumn(
                name: "GoogleMapPlaceCode",
                table: "businessTypes",
                newName: "GeoApiPlaceCategory");

            migrationBuilder.RenameColumn(
                name: "GeoApiPlaceCategry",
                table: "businessTypes",
                newName: "Designation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MapCode",
                table: "businessTypes",
                newName: "Label");

            migrationBuilder.RenameColumn(
                name: "GeoApiPlaceCategory",
                table: "businessTypes",
                newName: "GoogleMapPlaceCode");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "businessTypes",
                newName: "GeoApiPlaceCategry");
        }
    }
}
