using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class ChangesInFiltersTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "AdditionalFeaturesId",
                table: "Filters",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "AdditionalFeatures",
                table: "Filters",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filters_AdditionalFeatures_AdditionalFeaturesId",
                table: "Filters");

            migrationBuilder.DropIndex(
                name: "IX_Filters_AdditionalFeaturesId",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "AdditionalFeatures",
                table: "Filters");

            migrationBuilder.AlterColumn<bool>(
                name: "AdditionalFeaturesId",
                table: "Filters",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdditionalFeaturesId1",
                table: "Filters",
                type: "int",
                nullable: true);

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
    }
}
