using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class PlantMessagesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAnswers_Messages_MessageAnswerId",
                table: "MessageAnswers");

            migrationBuilder.AddColumn<bool>(
                name: "isNewPlant",
                table: "PlantMessages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isSeed",
                table: "PlantMessages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isSeedling",
                table: "PlantMessages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAnswers_Messages_MessageAnswerId",
                table: "MessageAnswers",
                column: "MessageAnswerId",
                principalTable: "Messages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAnswers_Messages_MessageAnswerId",
                table: "MessageAnswers");

            migrationBuilder.DropColumn(
                name: "isNewPlant",
                table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "isSeed",
                table: "PlantMessages");

            migrationBuilder.DropColumn(
                name: "isSeedling",
                table: "PlantMessages");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAnswers_Messages_MessageAnswerId",
                table: "MessageAnswers",
                column: "MessageAnswerId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
