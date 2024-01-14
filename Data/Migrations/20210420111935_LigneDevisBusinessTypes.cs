using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class LigneDevisBusinessTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NbrBusinesses",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PenetrationRate",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalCost",
                table: "Quotes",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "BusinessTypeQuoteLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessTypeId = table.Column<int>(type: "int", nullable: false),
                    BusinessTypeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessCost = table.Column<float>(type: "real", nullable: false),
                    QuoteId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTypeQuoteLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessTypeQuoteLines_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTypeQuoteLines_QuoteId",
                table: "BusinessTypeQuoteLines",
                column: "QuoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTypeQuoteLines");

            migrationBuilder.DropColumn(
                name: "NbrBusinesses",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "PenetrationRate",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Quotes");
        }
    }
}
