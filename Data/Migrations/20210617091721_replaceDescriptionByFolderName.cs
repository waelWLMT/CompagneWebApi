using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class replaceDescriptionByFolderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Photos",
                newName: "FileFolder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileFolder",
                table: "Photos",
                newName: "Description");
        }
    }
}
