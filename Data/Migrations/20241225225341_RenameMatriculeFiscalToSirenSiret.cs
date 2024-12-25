using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RenameMatriculeFiscalToSirenSiret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxIdNumber",
                table: "Customers",
                newName: "SirenSiret");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SirenSiret",
                table: "Customers",
                newName: "TaxIdNumber");
        }
    }
}
