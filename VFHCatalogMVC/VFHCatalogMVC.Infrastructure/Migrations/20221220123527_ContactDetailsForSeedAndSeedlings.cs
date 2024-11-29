using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class ContactDetailsForSeedAndSeedlings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactDetailForSeedlings",
                columns: table => new
                {
                    PlantSeedlingId = table.Column<int>(nullable: false),
                    ContactDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailForSeedlings", x => new { x.PlantSeedlingId, x.ContactDetailId });
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeedlings_ContactDetails_ContactDetailId",
                        column: x => x.ContactDetailId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeedlings_PlantSeedlings_PlantSeedlingId",
                        column: x => x.PlantSeedlingId,
                        principalTable: "PlantSeedlings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailForSeeds",
                columns: table => new
                {
                    PlantSeedId = table.Column<int>(nullable: false),
                    ContactDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailForSeeds", x => new { x.PlantSeedId, x.ContactDetailId });
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeeds_ContactDetails_ContactDetailId",
                        column: x => x.ContactDetailId,
                        principalTable: "ContactDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactDetailForSeeds_PlantSeeds_PlantSeedId",
                        column: x => x.PlantSeedId,
                        principalTable: "PlantSeeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailForSeedlings_ContactDetailId",
                table: "ContactDetailForSeedlings",
                column: "ContactDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailForSeeds_ContactDetailId",
                table: "ContactDetailForSeeds",
                column: "ContactDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactDetailForSeedlings");

            migrationBuilder.DropTable(
                name: "ContactDetailForSeeds");
        }
    }
}
