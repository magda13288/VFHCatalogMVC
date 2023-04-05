using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class ChangesInFiltersTable4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId",
            //    table: "Filters");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Filters_Colors_ColorId",
            //    table: "Filters");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Filters_Destinations_DestinationId",
            //    table: "Filters");

            migrationBuilder.DropForeignKey(
                name: "FK_Filters_GrowingSeazons_GrowingSeazonId",
                table: "Filters");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Filters_Positions_PositionId",
            //    table: "Filters");

            //migrationBuilder.DropIndex(
            //    name: "IX_Filters_AdditionalFeaturesId",
            //    table: "Filters");

            //migrationBuilder.DropIndex(
            //    name: "IX_Filters_ColorId",
            //    table: "Filters");

            //migrationBuilder.DropIndex(
            //    name: "IX_Filters_DestinationId",
            //    table: "Filters");

            migrationBuilder.DropIndex(
                name: "IX_Filters_GrowingSeazonId",
                table: "Filters");

            //migrationBuilder.DropIndex(
            //    name: "IX_Filters_PositionId",
            //    table: "Filters");

            //migrationBuilder.DropColumn(
            //    name: "AdditionalFeaturesId",
            //    table: "Filters");

            //migrationBuilder.DropColumn(
            //    name: "ColorId",
            //    table: "Filters");

            //migrationBuilder.DropColumn(
            //    name: "DestinationId",
            //    table: "Filters");

            migrationBuilder.DropColumn(
                name: "GrowingSeazonId",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "GrowingSeazonVisible",
                table: "Filters");

            //migrationBuilder.DropColumn(
            //    name: "PositionId",
            //    table: "Filters");

            migrationBuilder.AddColumn<bool>(
                name: "GrowingSeazon",
                table: "Filters",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrowingSeazon",
                table: "Filters");

            //migrationBuilder.AddColumn<int>(
            //    name: "AdditionalFeaturesId",
            //    table: "Filters",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "ColorId",
            //    table: "Filters",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "DestinationId",
            //    table: "Filters",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "GrowingSeazonId",
            //    table: "Filters",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "GrowingSeazonVisible",
            //    table: "Filters",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<int>(
            //    name: "PositionId",
            //    table: "Filters",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Filters_AdditionalFeaturesId",
            //    table: "Filters",
            //    column: "AdditionalFeaturesId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Filters_ColorId",
            //    table: "Filters",
            //    column: "ColorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Filters_DestinationId",
            //    table: "Filters",
            //    column: "DestinationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Filters_GrowingSeazonId",
            //    table: "Filters",
            //    column: "GrowingSeazonId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Filters_PositionId",
            //    table: "Filters",
            //    column: "PositionId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId",
            //    table: "Filters",
            //    column: "AdditionalFeaturesId",
            //    principalTable: "AdditionalFeatures",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Filters_Colors_ColorId",
            //    table: "Filters",
            //    column: "ColorId",
            //    principalTable: "Colors",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Filters_Destinations_DestinationId",
            //    table: "Filters",
            //    column: "DestinationId",
            //    principalTable: "Destinations",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Filters_GrowingSeazons_GrowingSeazonId",
            //    table: "Filters",
            //    column: "GrowingSeazonId",
            //    principalTable: "GrowingSeazons",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Filters_Positions_PositionId",
            //    table: "Filters",
            //    column: "PositionId",
            //    principalTable: "Positions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
