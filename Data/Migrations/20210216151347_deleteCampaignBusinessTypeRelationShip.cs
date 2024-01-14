using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class deleteCampaignBusinessTypeRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignProductType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignProductType",
                columns: table => new
                {
                    ProductTypeCampaignsId = table.Column<int>(type: "int", nullable: false),
                    ProductTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignProductType", x => new { x.ProductTypeCampaignsId, x.ProductTypesId });
                    table.ForeignKey(
                        name: "FK_CampaignProductType_Campaigns_ProductTypeCampaignsId",
                        column: x => x.ProductTypeCampaignsId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignProductType_ProductTypes_ProductTypesId",
                        column: x => x.ProductTypesId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignProductType_ProductTypesId",
                table: "CampaignProductType",
                column: "ProductTypesId");
        }
    }
}
