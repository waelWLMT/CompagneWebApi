using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ModifCampagnBusinessPK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CampaignBusinessBusinessTypeId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "CampaignBusinessCompagnId",
                table: "Photos",
                newName: "CampaignBusinessId");

            migrationBuilder.AddColumn<int>(
                name: "CampaignBusinessId",
                table: "CompaignBusinesses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompaignBusinesses",
                table: "CompaignBusinesses",
                column: "CampaignBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CampaignBusinessId",
                table: "Photos",
                column: "CampaignBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaignBusinesses_BusinessTypeId",
                table: "CompaignBusinesses",
                column: "BusinessTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_CompaignBusinesses_CampaignBusinessId",
                table: "Photos",
                column: "CampaignBusinessId",
                principalTable: "CompaignBusinesses",
                principalColumn: "CampaignBusinessId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_CompaignBusinesses_CampaignBusinessId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CampaignBusinessId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompaignBusinesses",
                table: "CompaignBusinesses");

            migrationBuilder.DropIndex(
                name: "IX_CompaignBusinesses_BusinessTypeId",
                table: "CompaignBusinesses");

            migrationBuilder.DropColumn(
                name: "CampaignBusinessId",
                table: "CompaignBusinesses");

            migrationBuilder.RenameColumn(
                name: "CampaignBusinessId",
                table: "Photos",
                newName: "CampaignBusinessCompagnId");

            migrationBuilder.AddColumn<int>(
                name: "CampaignBusinessBusinessTownId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignBusinessBusinessTypeId",
                table: "Photos",
                type: "int",
                nullable: true);

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
    }
}
