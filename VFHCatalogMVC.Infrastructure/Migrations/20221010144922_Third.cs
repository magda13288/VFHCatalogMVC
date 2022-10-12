using Microsoft.EntityFrameworkCore.Migrations;

namespace VFHCatalogMVC.Infrastructure.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantOpinions_PrivateUsers_PrivateUserId",
                table: "PlantOpinions");

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "Plants",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrivateUserId",
                table: "PlantOpinions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "PlantOpinions",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FruitTypeId",
                table: "PlantDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FruitSizeId",
                table: "PlantDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "PlantDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "GrowthTypes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlantGroupId",
                table: "GrowthTypes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "FruitTypes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlantGroupId",
                table: "FruitTypes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "FruitSizes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlantGroupId",
                table: "FruitSizes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PlantOpinions_CustomerId",
                table: "PlantOpinions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantOpinions_Customers_CustomerId",
                table: "PlantOpinions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantOpinions_PrivateUsers_PrivateUserId",
                table: "PlantOpinions",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantOpinions_Customers_CustomerId",
                table: "PlantOpinions");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantOpinions_PrivateUsers_PrivateUserId",
                table: "PlantOpinions");

            migrationBuilder.DropIndex(
                name: "IX_PlantOpinions_CustomerId",
                table: "PlantOpinions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PlantOpinions");

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "Plants",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrivateUserId",
                table: "PlantOpinions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FruitTypeId",
                table: "PlantDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FruitSizeId",
                table: "PlantDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "PlantDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "GrowthTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantGroupId",
                table: "GrowthTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "FruitTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantGroupId",
                table: "FruitTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantSectionId",
                table: "FruitSizes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlantGroupId",
                table: "FruitSizes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantOpinions_PrivateUsers_PrivateUserId",
                table: "PlantOpinions",
                column: "PrivateUserId",
                principalTable: "PrivateUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
