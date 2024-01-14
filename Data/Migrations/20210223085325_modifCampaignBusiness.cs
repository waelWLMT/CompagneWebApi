using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class modifCampaignBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlaceId",
                table: "CompaignBusinesses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    PlaceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompaignBusinesses_Place_PlaceId",
                table: "CompaignBusinesses");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropIndex(
                name: "IX_CompaignBusinesses_PlaceId",
                table: "CompaignBusinesses");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "CompaignBusinesses");
        }
    }
}
