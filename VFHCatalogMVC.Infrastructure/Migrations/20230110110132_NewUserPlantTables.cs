using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class NewUserPlantTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndexPlantMessages",
                columns: table => new
                {
                    PlantId = table.Column<int>(nullable: false),
                    MessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewUserPlantMessages", x => new { x.PlantId, x.MessageId });
                    table.ForeignKey(
                        name: "FK_NewUserPlantMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewUserPlantMessages_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewUserPlants",
                columns: table => new
                {
                    PlantId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewUserPlants", x => new { x.PlantId, x.UserId });
                    table.ForeignKey(
                        name: "FK_NewUserPlants_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewUserPlants_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewUserPlantMessages_MessageId",
                table: "IndexPlantMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewUserPlants_UserId",
                table: "NewUserPlants",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IndexPlantMessages");

            migrationBuilder.DropTable(
                name: "NewUserPlants");
        }
    }
}
