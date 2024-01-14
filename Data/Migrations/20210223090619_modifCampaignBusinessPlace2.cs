using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class modifCampaignBusinessPlace2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaignBusinesses_Place_PlaceId",
                table: "CompaignBusinesses");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropIndex(
                name: "IX_CompaignBusinesses_PlaceId",
                table: "CompaignBusinesses");

            migrationBuilder.RenameColumn(
                name: "PlaceId",
                table: "CompaignBusinesses",
                newName: "Place_PlaceId");

            migrationBuilder.AlterColumn<string>(
                name: "Place_PlaceId",
                table: "CompaignBusinesses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Place_Lat",
                table: "CompaignBusinesses",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Place_Lng",
                table: "CompaignBusinesses",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place_Name",
                table: "CompaignBusinesses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place_Lat",
                table: "CompaignBusinesses");

            migrationBuilder.DropColumn(
                name: "Place_Lng",
                table: "CompaignBusinesses");

            migrationBuilder.DropColumn(
                name: "Place_Name",
                table: "CompaignBusinesses");

            migrationBuilder.RenameColumn(
                name: "Place_PlaceId",
                table: "CompaignBusinesses",
                newName: "PlaceId");

            migrationBuilder.AlterColumn<string>(
                name: "PlaceId",
                table: "CompaignBusinesses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    PlaceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.PlaceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompaignBusinesses_PlaceId",
                table: "CompaignBusinesses",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompaignBusinesses_Place_PlaceId",
                table: "CompaignBusinesses",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "PlaceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
