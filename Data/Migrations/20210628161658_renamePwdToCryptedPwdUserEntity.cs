using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class renamePwdToCryptedPwdUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "CryptedPassword");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CryptedPassword",
                table: "Users",
                newName: "Password");
        }
    }
}
