using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class NewTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_IndexPlantMessages_Messages_MessageId",
            //    table: "IndexPlantMessages");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_IndexPlantMessages_Plants_PlantId",
            //    table: "IndexPlantMessages");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_IndexPlantMessages",
            //    table: "IndexPlantMessages");

            //migrationBuilder.RenameTable(
            //    name: "IndexPlantMessages",
            //    newName: "PlantMessages");

            //migrationBuilder.RenameIndex(
            //    name: "IX_IndexPlantMessages_MessageId",
            //    table: "PlantMessages",
            //    newName: "IX_PlantMessages_MessageId");

            migrationBuilder.AddColumn<int>(
                name: "AdditionalFeaturesId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GrowingSeazonId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GrowthTypeId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeightId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PollinationId",
                table: "PlantDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "PlantDetails",
                nullable: true);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_PlantMessages",
            //    table: "PlantMessages",
            //    columns: new[] { "PlantId", "MessageId" });

            migrationBuilder.CreateTable(
                name: "AdditionalFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Heights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pollinations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pollinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantTypeId = table.Column<int>(nullable: false),
                    PlantGroupId = table.Column<int>(nullable: true),
                    PlantSectionId = table.Column<int>(nullable: true),
                    ColorId = table.Column<int>(nullable: true),
                    DestinationId = table.Column<int>(nullable: true),
                    FruitSizeId = table.Column<int>(nullable: true),
                    FruitTypeId = table.Column<int>(nullable: true),
                    GrowingSeazonId = table.Column<int>(nullable: true),
                    GrowthTypeId = table.Column<int>(nullable: true),
                    HeightId = table.Column<int>(nullable: true),
                    PollinationId = table.Column<int>(nullable: true),
                    PositionId = table.Column<int>(nullable: true),
                    AdditionalFeaturesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId",
                        column: x => x.AdditionalFeaturesId,
                        principalTable: "AdditionalFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_FruitSizes_FruitSizeId",
                        column: x => x.FruitSizeId,
                        principalTable: "FruitSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_FruitTypes_FruitTypeId",
                        column: x => x.FruitTypeId,
                        principalTable: "FruitTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_GrowingSeazons_GrowingSeazonId",
                        column: x => x.GrowingSeazonId,
                        principalTable: "GrowingSeazons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_GrowthTypes_GrowthTypeId",
                        column: x => x.GrowthTypeId,
                        principalTable: "GrowthTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_Heights_HeightId",
                        column: x => x.HeightId,
                        principalTable: "Heights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_PlantGroups_PlantGroupId",
                        column: x => x.PlantGroupId,
                        principalTable: "PlantGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_PlantSections_PlantSectionId",
                        column: x => x.PlantSectionId,
                        principalTable: "PlantSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_PlantTypes_PlantTypeId",
                        column: x => x.PlantTypeId,
                        principalTable: "PlantTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Filters_Pollinations_PollinationId",
                        column: x => x.PollinationId,
                        principalTable: "Pollinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filters_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantPositions",
                columns: table => new
                {
                    PositionId = table.Column<int>(nullable: false),
                    PlantDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantPositions", x => new { x.PlantDetailId, x.PositionId });
                    table.ForeignKey(
                        name: "FK_PlantPositions_PlantDetails_PlantDetailId",
                        column: x => x.PlantDetailId,
                        principalTable: "PlantDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantPositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_AdditionalFeaturesId",
                table: "PlantDetails",
                column: "AdditionalFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_DestinationId",
                table: "PlantDetails",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_GrowingSeazonId",
                table: "PlantDetails",
                column: "GrowingSeazonId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_GrowthTypeId",
                table: "PlantDetails",
                column: "GrowthTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_HeightId",
                table: "PlantDetails",
                column: "HeightId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_PollinationId",
                table: "PlantDetails",
                column: "PollinationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantDetails_PositionId",
                table: "PlantDetails",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_AdditionalFeaturesId",
                table: "Filters",
                column: "AdditionalFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ColorId",
                table: "Filters",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_DestinationId",
                table: "Filters",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_FruitSizeId",
                table: "Filters",
                column: "FruitSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_FruitTypeId",
                table: "Filters",
                column: "FruitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_GrowingSeazonId",
                table: "Filters",
                column: "GrowingSeazonId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_GrowthTypeId",
                table: "Filters",
                column: "GrowthTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_HeightId",
                table: "Filters",
                column: "HeightId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_PlantGroupId",
                table: "Filters",
                column: "PlantGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_PlantSectionId",
                table: "Filters",
                column: "PlantSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_PlantTypeId",
                table: "Filters",
                column: "PlantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_PollinationId",
                table: "Filters",
                column: "PollinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_PositionId",
                table: "Filters",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantPositions_PositionId",
                table: "PlantPositions",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_AdditionalFeatures_AdditionalFeaturesId",
                table: "PlantDetails",
                column: "AdditionalFeaturesId",
                principalTable: "AdditionalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_Destinations_DestinationId",
                table: "PlantDetails",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_GrowingSeazons_GrowingSeazonId",
                table: "PlantDetails",
                column: "GrowingSeazonId",
                principalTable: "GrowingSeazons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_GrowthTypes_GrowthTypeId",
                table: "PlantDetails",
                column: "GrowthTypeId",
                principalTable: "GrowthTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_Heights_HeightId",
                table: "PlantDetails",
                column: "HeightId",
                principalTable: "Heights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_Pollinations_PollinationId",
                table: "PlantDetails",
                column: "PollinationId",
                principalTable: "Pollinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantDetails_Positions_PositionId",
                table: "PlantDetails",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PlantMessages_Messages_MessageId",
            //    table: "PlantMessages",
            //    column: "MessageId",
            //    principalTable: "Messages",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PlantMessages_Plants_PlantId",
            //    table: "PlantMessages",
            //    column: "PlantId",
            //    principalTable: "Plants",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_AdditionalFeatures_AdditionalFeaturesId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_Destinations_DestinationId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_GrowingSeazons_GrowingSeazonId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_GrowthTypes_GrowthTypeId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_Heights_HeightId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_Pollinations_PollinationId",
                table: "PlantDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantDetails_Positions_PositionId",
                table: "PlantDetails");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PlantMessages_Messages_MessageId",
            //    table: "PlantMessages");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_PlantMessages_Plants_PlantId",
            //    table: "PlantMessages");

            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "PlantPositions");

            migrationBuilder.DropTable(
                name: "AdditionalFeatures");

            migrationBuilder.DropTable(
                name: "Heights");

            migrationBuilder.DropTable(
                name: "Pollinations");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_AdditionalFeaturesId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_DestinationId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_GrowingSeazonId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_GrowthTypeId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_HeightId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_PollinationId",
                table: "PlantDetails");

            migrationBuilder.DropIndex(
                name: "IX_PlantDetails_PositionId",
                table: "PlantDetails");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_PlantMessages",
            //    table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "AdditionalFeaturesId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "GrowingSeazonId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "GrowthTypeId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "HeightId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "PollinationId",
                table: "PlantDetails");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "PlantDetails");

            //migrationBuilder.RenameTable(
            //    name: "PlantMessages",
            //    newName: "IndexPlantMessages");

            //migrationBuilder.RenameIndex(
            //    name: "IX_PlantMessages_MessageId",
            //    table: "IndexPlantMessages",
            //    newName: "IX_IndexPlantMessages_MessageId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_IndexPlantMessages",
            //    table: "IndexPlantMessages",
            //    columns: new[] { "PlantId", "MessageId" });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_IndexPlantMessages_Messages_MessageId",
            //    table: "IndexPlantMessages",
            //    column: "MessageId",
            //    principalTable: "Messages",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_IndexPlantMessages_Plants_PlantId",
            //    table: "IndexPlantMessages",
            //    column: "PlantId",
            //    principalTable: "Plants",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
