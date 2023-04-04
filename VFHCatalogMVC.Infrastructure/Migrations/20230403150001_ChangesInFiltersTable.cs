using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class ChangesInFiltersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId",
                table: "Filters");

            migrationBuilder.DropIndex(
                name: "IX_Filters_AdditionalFeaturesId",
                table: "Filters");

            migrationBuilder.AlterColumn<bool>(
                name: "AdditionalFeaturesId",
                table: "Filters",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalFeaturesId1",
                table: "Filters",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Color",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Destination",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FruitSizeVisible",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FruitTypeVisible",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GrowingSeazonVisible",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GrowthTypeVisible",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HeightVisible",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PollinationVisible",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Position",
                table: "Filters",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Filters_AdditionalFeaturesId1",
                table: "Filters",
                column: "AdditionalFeaturesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId1",
                table: "Filters",
                column: "AdditionalFeaturesId1",
                principalTable: "AdditionalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId1",
                table: "Filters");

            migrationBuilder.DropIndex(
                name: "IX_Filters_AdditionalFeaturesId1",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "AdditionalFeaturesId1",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "FruitSizeVisible",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "FruitTypeVisible",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "GrowingSeazonVisible",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "GrowthTypeVisible",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "HeightVisible",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "PollinationVisible",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Filters");

            migrationBuilder.AlterColumn<int>(
                name: "AdditionalFeaturesId",
                table: "Filters",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.CreateIndex(
                name: "IX_Filters_AdditionalFeaturesId",
                table: "Filters",
                column: "AdditionalFeaturesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId",
                table: "Filters",
                column: "AdditionalFeaturesId",
                principalTable: "AdditionalFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
