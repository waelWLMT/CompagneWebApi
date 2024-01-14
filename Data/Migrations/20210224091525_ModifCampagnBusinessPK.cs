using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ModifCampagnBusinessPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_CompaignBusinesses_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompaignBusinesses",
                table: "CompaignBusinesses");

            migrationBuilder.AddColumn<int>(
                name: "CampaignBusinessBusinessTownId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusinessTownId",
                table: "CompaignBusinesses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompaignBusinesses",
                table: "CompaignBusinesses",
                columns: new[] { "BusinessTypeId", "CompagnId", "BusinessTownId" });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId_CampaignBusinessBusinessTownId",
                table: "Photos",
                columns: new[] { "CampaignBusinessBusinessTypeId", "CampaignBusinessCompagnId", "CampaignBusinessBusinessTownId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_CompaignBusinesses_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId_CampaignBusinessBusinessTownId",
                table: "Photos",
                columns: new[] { "CampaignBusinessBusinessTypeId", "CampaignBusinessCompagnId", "CampaignBusinessBusinessTownId" },
                principalTable: "CompaignBusinesses",
                principalColumns: new[] { "BusinessTypeId", "CompagnId", "BusinessTownId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_CompaignBusinesses_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId_CampaignBusinessBusinessTownId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId_CampaignBusinessBusinessTownId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompaignBusinesses",
                table: "CompaignBusinesses");

            migrationBuilder.DropColumn(
                name: "CampaignBusinessBusinessTownId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "BusinessTownId",
                table: "CompaignBusinesses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompaignBusinesses",
                table: "CompaignBusinesses",
                columns: new[] { "BusinessTypeId", "CompagnId" });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId",
                table: "Photos",
                columns: new[] { "CampaignBusinessBusinessTypeId", "CampaignBusinessCompagnId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_CompaignBusinesses_CampaignBusinessBusinessTypeId_CampaignBusinessCompagnId",
                table: "Photos",
                columns: new[] { "CampaignBusinessBusinessTypeId", "CampaignBusinessCompagnId" },
                principalTable: "CompaignBusinesses",
                principalColumns: new[] { "BusinessTypeId", "CompagnId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
