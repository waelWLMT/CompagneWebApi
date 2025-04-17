using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ModifieBusinessTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "businessTypes");

            migrationBuilder.RenameColumn(
                name: "MapCode",
                table: "businessTypes",
                newName: "TagValueDesignation");

            migrationBuilder.RenameColumn(
                name: "GeoApiPlaceCode",
                table: "businessTypes",
                newName: "TagValueCode");

            migrationBuilder.RenameColumn(
                name: "GeoApiPlaceCategory",
                table: "businessTypes",
                newName: "TagKeyDesignation");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "businessTypes",
                newName: "TagKeyCode");

            migrationBuilder.RenameColumn(
                name: "BusinessTypeCode",
                table: "BusinessTypeQuoteLines",
                newName: "BusinessTypeDesignation");

            migrationBuilder.AddColumn<string>(
                name: "Address_HouseNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_PostalCode",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_HouseNumber",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Address_PostalCode",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "TagValueDesignation",
                table: "businessTypes",
                newName: "MapCode");

            migrationBuilder.RenameColumn(
                name: "TagValueCode",
                table: "businessTypes",
                newName: "GeoApiPlaceCode");

            migrationBuilder.RenameColumn(
                name: "TagKeyDesignation",
                table: "businessTypes",
                newName: "GeoApiPlaceCategory");

            migrationBuilder.RenameColumn(
                name: "TagKeyCode",
                table: "businessTypes",
                newName: "Designation");

            migrationBuilder.RenameColumn(
                name: "BusinessTypeDesignation",
                table: "BusinessTypeQuoteLines",
                newName: "BusinessTypeCode");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "businessTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
