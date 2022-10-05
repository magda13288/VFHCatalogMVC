using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_Colors_ColorId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_FruitSizes_FruitSizeId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_FruitTypes_FruitTypeId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_GrowingSeazons_GrowingSeazonId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_GrowingSeazonId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "GrowingSeazonId",
                table: "PlantDetails");

            migrationBuilder.CreateTable(
                name: "PlantGrowingSeazons",
                columns: table => new
                {
                    GrowingSeazonId = table.Column<int>(nullable: false),
                    PlantDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantGrowingSeazons", x => new { x.PlantDetailId, x.GrowingSeazonId });
                    table.ForeignKey(
                        name: "FK_PlantGrowingSeazons_GrowingSeazons_GrowingSeazonId",
                        column: x => x.GrowingSeazonId,
                        principalTable: "GrowingSeazons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantGrowingSeazons_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantGrowingSeazons_GrowingSeazonId",
                table: "PlantGrowingSeazons",
                column: "GrowingSeazonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_Colors_ColorId",
                table: "PlantDetails",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_FruitSizes_FruitSizeId",
                table: "PlantDetails",
                column: "FruitSizeId",
                principalTable: "FruitSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_FruitTypes_FruitTypeId",
                table: "PlantDetails",
                column: "FruitTypeId",
                principalTable: "FruitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_Colors_ColorId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_FruitSizes_FruitSizeId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_FruitTypes_FruitTypeId",
                table: "PlantDetails");

            migrationBuilder.DropTable(
                name: "PlantGrowingSeazons");

            migrationBuilder.AddColumn<int>(
                name: "GrowingSeazonId",
                table: "PlantDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_GrowingSeazonId",
                table: "PlantDetails",
                column: "GrowingSeazonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_Colors_ColorId",
                table: "PlantDetails",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_FruitSizes_FruitSizeId",
                table: "PlantDetails",
                column: "FruitSizeId",
                principalTable: "FruitSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_FruitTypes_FruitTypeId",
                table: "PlantDetails",
                column: "FruitTypeId",
                principalTable: "FruitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_GrowingSeazons_GrowingSeazonId",
                table: "PlantDetails",
                column: "GrowingSeazonId",
                principalTable: "GrowingSeazons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
