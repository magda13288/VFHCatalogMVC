using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class NewTablesProducerAndSoilPh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProducerId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoilPhId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Producer",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SoilPh",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoilPhs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilPhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantProducers",
                columns: table => new
                {
                    ProducerId = table.Column<int>(nullable: false),
                    PlantDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantProducers", x => new { x.ProducerId, x.PlantDetailId });
                    table.ForeignKey(
                        name: "FK_PlantProducers_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantProducers_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantSoilPhs",
                columns: table => new
                {
                    SoilPhId = table.Column<int>(nullable: false),
                    PlantDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSoilPhs", x => new { x.SoilPhId, x.PlantDetailId });
                    table.ForeignKey(
                        name: "FK_PlantSoilPhs_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantSoilPhs_SoilPhs_SoilPhId",
                        column: x => x.SoilPhId,
                        principalTable: "SoilPhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_ProducerId",
                table: "PlantDetails",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_SoilPhId",
                table: "PlantDetails",
                column: "SoilPhId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantProducers_PlantDetailId",
                table: "PlantProducers",
                column: "PlantDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantSoilPhs_PlantDetailId",
                table: "PlantSoilPhs",
                column: "PlantDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_Producers_ProducerId",
                table: "PlantDetails",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_SoilPhs_SoilPhId",
                table: "PlantDetails",
                column: "SoilPhId",
                principalTable: "SoilPhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_Producers_ProducerId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_SoilPhs_SoilPhId",
                table: "PlantDetails");

            migrationBuilder.DropTable(
                name: "PlantProducers");

            migrationBuilder.DropTable(
                name: "PlantSoilPhs");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "SoilPhs");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_ProducerId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_SoilPhId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "SoilPhId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "Producer",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "SoilPh",
                table: "Filters");
        }
    }
}
